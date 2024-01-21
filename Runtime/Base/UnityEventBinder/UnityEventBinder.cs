using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kadinche.Kassets.EventSystem
{
    public class UnityEventBinder : MonoBehaviour
    {
        [SerializeField] protected GameEvent gameEventToListen;
        [Space, SerializeField] private UnityEvent _onGameEventRaised;

        protected readonly List<IDisposable> subscriptions = new List<IDisposable>();

        protected virtual void Start()
        {
            subscriptions.Add(gameEventToListen.Subscribe(_onGameEventRaised.Invoke));
        }

        protected virtual void OnDestroy()
        {
            subscriptions.ForEach(subscription => subscription.Dispose());
        }
    }

    public abstract class UnityEventBinder<T> : UnityEventBinder
    {
        [SerializeField] protected TypedUnityEvent onTypedGameEventRaised;
        
        protected override void Start()
        {
            base.Start();
            if (gameEventToListen is GameEvent<T> typedEvent)
            {
                subscriptions.Add(typedEvent.Subscribe(onTypedGameEventRaised.Invoke));
            }
        }

        [Serializable]
        protected class TypedUnityEvent : UnityEvent<T> { }
    }
}