using Cysharp.Threading.Tasks;
using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace Kadinche.Kassets.Components
{
    public class GameEventsForwarder : MonoBehaviour
    {
        [SerializeField] private GameEventCollection _eventsToSubscribe;
        [SerializeField] private GameEventCollection _eventsToRaise;

        private void Start()
        {
            _eventsToSubscribe.SubscribeAnyEvent(_eventsToRaise.RaiseAllEvents, this.GetCancellationTokenOnDestroy());
        }
    }
}