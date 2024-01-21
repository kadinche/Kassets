using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kadinche.Kassets.EventSystem
{
    public class UnityEventBatchBinder : MonoBehaviour
    {
        [SerializeField] private GameEvent[] _gameEventsToListen;
        [Space, SerializeField] private UnityEvent _onGameEventRaised;

        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();

        protected virtual void Start()
        {
            foreach (var gameEventToListen in _gameEventsToListen)
            {
                _subscriptions.Add(gameEventToListen.Subscribe(_onGameEventRaised.Invoke));   
            }
        }

        protected virtual void OnDestroy()
        {
            _subscriptions.ForEach(subscription => subscription.Dispose());
        }
    }
}