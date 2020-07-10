using Cysharp.Threading.Tasks;
using Kassets.EventSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Kassets.UnityComponents
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