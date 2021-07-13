using Cysharp.Threading.Tasks;
using Kadinche.Kassets.EventSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Kadinche.Kassets.UnityComponents
{
    public class GameEventsToUnityEventForwarder : MonoBehaviour
    {
        [SerializeField] private GameEventCollection _eventsToSubscribe;
        [SerializeField] private UnityEvent _unityEventToRaise;

        private void Start()
        {
            _eventsToSubscribe.SubscribeAnyEvent(_unityEventToRaise.Invoke, this.GetCancellationTokenOnDestroy());
        }
    }
}