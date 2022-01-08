using System;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    public class BoolUnityEventBinder : UnityEventBinder<bool>
    {
        [SerializeField] private TypedUnityEvent _onNegatedBoolEventRaised;

        protected override void Start()
        {
            base.Start();
            if (gameEventToListen is GameEvent<bool> typedEvent)
            {
                subscriptions.Add(typedEvent.Subscribe(value => _onNegatedBoolEventRaised.Invoke(!value)));
            }
        }
    }
}