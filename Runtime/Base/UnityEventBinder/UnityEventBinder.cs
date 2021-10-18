using System;
using UnityEngine;
using UnityEngine.Events;

namespace Kadinche.Kassets.EventSystem
{
    public class UnityEventBinder : MonoBehaviour
    {
        [SerializeField] protected GameEvent _gameEventToListen;
        [Space]
        [SerializeField] private UnityEvent _onGameEventRaised;

        private IDisposable _subscription;

        protected virtual void Start()
        {
            _subscription = _gameEventToListen.Subscribe(_onGameEventRaised.Invoke);
        }

        protected virtual void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }

    public abstract class UnityEventBinder<T> : UnityEventBinder
    {
        [SerializeField] private TypedUnityEvent _onTypedGameEventRaised;

        private IDisposable _subscription;

        protected override void Start()
        {
            base.Start();
            if (_gameEventToListen is GameEvent<T> typedEvent)
            {
                _subscription = typedEvent.Subscribe(_onTypedGameEventRaised.Invoke);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _subscription?.Dispose();
        }

        [Serializable]
        private class TypedUnityEvent : UnityEvent<T> { }
    }
}