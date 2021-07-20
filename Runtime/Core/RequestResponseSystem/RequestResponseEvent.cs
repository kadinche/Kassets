using System;
using System.Collections.Generic;
using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    public abstract class RequestResponseEvent<TRequest, TResponse> : GameEvent<TRequest>
    {
        private readonly Queue<Tuple<TRequest, Action<TResponse>>> _requests = new Queue<Tuple<TRequest, Action<TResponse>>>();
            
        private ResponseSubscription<TRequest, TResponse> _responseSubscription;
        
        public void Request(TRequest param, Action<TResponse> onResponse)
        {
            if (_responseSubscription is {disposed: false})
            {
                var response = _responseSubscription.Invoke(param);
                onResponse.Invoke(response);
            }
            else
            {
                _requests.Enqueue(new Tuple<TRequest, Action<TResponse>>(param, onResponse));
            }
        }

        public void Response(Func<TRequest, TResponse> responseFunc)
        {
            if (_requests.Count <= 0) return;
            
            var request = _requests.Dequeue();
            var response = responseFunc.Invoke(request.Item1);
            request.Item2.Invoke(response);
        }

        public IDisposable SubscribeResponse(Func<TRequest, TResponse> responseFunc, bool overrideResponse = true)
        {
            if (!overrideResponse && _responseSubscription != null)
            {
                Debug.LogWarning("Responder already exist.");
                return _responseSubscription;
            }
            
            _responseSubscription?.Dispose();

            if (_responseSubscription == null || _responseSubscription.disposed)
            {
                _responseSubscription = new ResponseSubscription<TRequest, TResponse>(responseFunc);
            }

            while (_requests.Count > 0)
            {
                Response(responseFunc);
            }

            return _responseSubscription;
        }

        public override void Dispose()
        {
            base.Dispose();
            _requests.Clear();
            _responseSubscription?.Dispose();
        }
    }

    public abstract class RequestResponseEvent<T> : RequestResponseEvent<T, T>
    {
    }
    
    internal class ResponseSubscription<TRequest, TResponse> : IDisposable
    {
        private readonly Func<TRequest, TResponse> _responseFunc;
        public bool disposed;
        
        public ResponseSubscription(
            Func<TRequest, TResponse> responseFunc)
        {
            _responseFunc = responseFunc;
        }

        public TResponse Invoke(TRequest param) => _responseFunc.Invoke(param);
        
        public void Dispose()
        {
            disposed = true;
        }
    }
}