using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    /// <summary>
    /// An event that contains collection of events. Get raised whenever any event is raised.
    /// Made it possible to listen to many events at once.
    /// </summary>
    [Serializable, Obsolete]
    public partial class GameEventCollection : IGameEventRaiser, IGameEventHandler, IDisposable
    {
        [SerializeField] private List<GameEvent> _gameEvents;
        [SerializeField] protected bool buffered;

        public IDisposable Subscribe(Action action) => Subscribe(action, buffered);

        public void Raise()
        {
            foreach (IGameEventRaiser gameEvent in _gameEvents)
            {
                gameEvent.Raise();
            }
        }
    }
    
#if !KASSETS_UNIRX && !KASSETS_R3
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
#endif

#if !KASSETS_UNITASK
    public partial class GameEventCollection
    {
        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
#endif
}