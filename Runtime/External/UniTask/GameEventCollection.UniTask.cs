#if KASSETS_UNITASK

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Kadinche.Kassets.EventSystem
{
    public partial class GameEventCollection
    {
        protected CancellationTokenSource cts = new CancellationTokenSource();

        public void Subscribe(Action onAnyEvent, CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            foreach (var gameEvent in _gameEvents)
            {
                gameEvent.Subscribe(onAnyEvent, token);
            }
        }

        public UniTask AnyEventAsync(CancellationToken cancellationToken = default)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            return UniTask.WhenAny(_gameEvents.Select(gameEvent => gameEvent.EventAsync(token)));
        }
        
        public void Dispose()
        {
            _compositeDisposable.Dispose();
            cts.CancelAndDispose();
        }
    }
}

#endif