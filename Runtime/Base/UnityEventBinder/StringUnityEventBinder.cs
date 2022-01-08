using System;

namespace Kadinche.Kassets.EventSystem
{
    public class StringUnityEventBinder : UnityEventBinder<string>
    {
        protected override void Start()
        {
            subscriptions.Add(gameEventToListen.Subscribe(() => onTypedGameEventRaised.Invoke(gameEventToListen.ToString())));
        }
    }
}