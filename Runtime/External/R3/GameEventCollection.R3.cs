#if KASSETS_R3

using System;
using R3;

namespace Kadinche.Kassets.EventSystem
{
    public partial class GameEventCollection
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public IDisposable Subscribe(Action onAnyEvent, bool withBuffer)
        {
            foreach (IGameEventHandler gameEvent in _gameEvents)
            {
                gameEvent.Subscribe(onAnyEvent).AddTo(_compositeDisposable);
            }

            if (withBuffer)
            {
                onAnyEvent.Invoke();
            }

            return _compositeDisposable;
        }
    }
}

#endif
