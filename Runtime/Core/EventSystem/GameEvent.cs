using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = MenuHelper.DefaultEventMenu + "GameEvent")]
    public class GameEvent : GameEventBase, IEventRaiser
    {
        /// <summary>
        /// Raise the event.
        /// </summary>
        public virtual void Raise()
        {
            foreach (var disposable in disposables)
            {
                if (disposable is Subscription listener)
                {
                    listener.Invoke();
                }
            }
        }

        public virtual void RaiseEditor() => Raise();
    }

    /// <summary>
    /// Generic base class for event system with parameter.
    /// </summary>
    /// <typeparam name="T">Parameter type for the event system</typeparam>
    public abstract class GameEvent<T> : GameEvent, IEventRaiser<T>, IEventHandler<T>
    {
        [SerializeField] protected T inspectorRaiseValue;
        
        public override void Raise()
        {
            base.Raise();
            Raise(inspectorRaiseValue);
        }

        /// <summary>
        /// Raise the event with parameter.
        /// </summary>
        /// /// <param name="param"></param>
        public void Raise(T param)
        {
            inspectorRaiseValue = param;
            foreach (var disposable in disposables)
            {
                if (disposable is Subscription<T> listener)
                {
                    listener.Invoke(inspectorRaiseValue);
                }
            }
        }
        
        public override void RaiseEditor() => Raise(inspectorRaiseValue);
        
        public IDisposable Subscribe(Action<T> action)
        {
            var listener = new Subscription<T>(action, disposables);
            if (!disposables.Contains(listener))
            {
                disposables.Add(listener);
                
                if (buffered)
                {
                    listener.Invoke(inspectorRaiseValue);
                }
            }

            return listener;
        }
    }
    
    /// <summary>
    /// An event that contains collection of events. Get raised whenever any event is raised.
    /// Made it possible to listen to many events at once.
    /// </summary>
    [Serializable]
    public class GameEventCollection : IEventRaiser, IEventHandler, IDisposable
    {
        [SerializeField] private List<GameEventBase> _gameEventBases;

        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public IDisposable Subscribe(Action onAnyEvent)
        {
            foreach (var gameEventBase in _gameEventBases)
            {
                _compositeDisposable.Add(gameEventBase.Subscribe(onAnyEvent));
            }

            return _compositeDisposable;
        }

        public void Raise()
        {
            foreach (var gameEventBase in _gameEventBases)
            {
                if (gameEventBase is GameEvent gameEvent)
                    gameEvent.Raise();
            }
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}