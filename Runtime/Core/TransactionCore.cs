#if !KASSETS_UNIRX && !KASSETS_UNITASK && !KASSETS_R3
#define KASSETS_STANDALONE
#endif

using System;
using System.Collections.Generic;
using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace Kadinche.Kassets.Transaction
{
    public abstract partial class TransactionCore<TRequest, TResponse> : GameEvent<TRequest>, IGameEventHandler<TResponse>
    {
        [SerializeField] protected TResponse responseValue;
        
        private readonly Queue<Tuple<TRequest, Action<TResponse>>> _requests =
            new Queue<Tuple<TRequest, Action<TResponse>>>();

        internal IDisposable responseSubscription;
        
        public Type ResponseType => typeof(TResponse);

        public void Request(Action onResponse) => Request(default, _ => onResponse.Invoke());

        public void Request(TRequest param, Action<TResponse> onResponse)
        {
            _requests.Enqueue(new Tuple<TRequest, Action<TResponse>>(param, onResponse));
            Raise(param);
            TryRespond();
        }
        
        public IDisposable RegisterResponse(Func<TRequest, TResponse> responseFunc, bool overrideResponse = true)
        {
            if (!overrideResponse && responseSubscription != null)
            {
                Debug.LogWarning("Registered Response already exist.");
                return responseSubscription;
            }

            responseSubscription?.Dispose();
            responseSubscription ??= HandleSubscribe(responseFunc);

            while (_requests.Count > 0)
            {
                TryRespond();
            }

            return responseSubscription;
        }

        protected override void ResetInternal()
        {
            base.ResetInternal();
            responseValue = default;
        }

        public IDisposable SubscribeToRequest(Action action) => Subscribe(action);
        public IDisposable SubscribeToRequest(Action<TRequest> action) => Subscribe(action);
        public IDisposable SubscribeToResponse(Action action) => HandleSubscribeToResponse(action);
        public IDisposable SubscribeToResponse(Action<TResponse> action) => Subscribe(action);
        public IDisposable Subscribe(Action<TResponse> action) => HandleSubscribeToResponse(action);
    }

    public abstract class TransactionCore<T> : TransactionCore<T, T>
    {
    }

#if !KASSETS_UNITASK
    public abstract partial class TransactionCore<TRequest, TResponse>
    {
        public void Response(Func<TRequest, TResponse> responseFunc)
        {
            if (_requests.Count <= 0) return;

            var request = _requests.Dequeue();
            var response = responseFunc.Invoke(request.Item1);
            request.Item2.Invoke(response);
            RaiseResponse(response);
        }
    }
#endif

#if KASSETS_STANDALONE
    public abstract partial class TransactionCore<TRequest, TResponse>
    {
        protected readonly List<IDisposable> responseSubscribers = new List<IDisposable>();

        private void TryRespond()
        {
            if (responseSubscription is ResponseSubscription<TRequest, TResponse> subscription)
            {
                Response(subscription.Invoke);
            }
        }

        private void RaiseResponse(TResponse param)
        {
            responseValue = param;
            foreach (var disposable in responseSubscribers)
            {
                if (disposable is Subscription<TResponse> typedSubscription)
                {
                    typedSubscription.Invoke(responseValue);
                }
                else if (disposable is Subscription subscription)
                {
                    subscription.Invoke();
                }
            }
        }

        private IDisposable HandleSubscribe(Func<TRequest, TResponse> responseFunc)
        {
            return new ResponseSubscription<TRequest, TResponse>(this, responseFunc);
        }

        private IDisposable HandleSubscribeToResponse(Action action)
        {
            var subscription = new Subscription(action, responseSubscribers);
            if (!responseSubscribers.Contains(subscription))
            {
                responseSubscribers.Add(subscription);

                if (buffered)
                {
                    subscription.Invoke();
                }
            }

            return subscription;
        }
        
        private IDisposable HandleSubscribeToResponse(Action<TResponse> action)
        {
            var subscription = new Subscription<TResponse>(action, responseSubscribers);
            if (!responseSubscribers.Contains(subscription))
            {
                responseSubscribers.Add(subscription);

                if (buffered)
                {
                    subscription.Invoke(responseValue);
                }
            }

            return subscription;
        }

        public override void Dispose()
        {
            base.Dispose();
            _requests.Clear();
            responseSubscription?.Dispose();
        }
    }
    
    internal class ResponseSubscription<TRequest, TResponse> : IDisposable
    {
        private TransactionCore<TRequest, TResponse> _source;
        private Func<TRequest, TResponse> _responseFunc;
        
        public ResponseSubscription(
            TransactionCore<TRequest, TResponse> source,
            Func<TRequest, TResponse> responseFunc)
        {
            _source = source;
            _responseFunc = responseFunc;
        }

        public TResponse Invoke(TRequest param) => _responseFunc.Invoke(param);
        
        public void Dispose()
        {
            _responseFunc = null;
            _source.responseSubscription = null;
            _source = null;
        }
    }
#endif
}