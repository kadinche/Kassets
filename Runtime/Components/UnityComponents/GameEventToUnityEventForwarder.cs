using Cysharp.Threading.Tasks;
using Kadinche.Kassets.EventSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Kadinche.Kassets.UnityComponents
{
    public class GameEventToUnityEventForwarder : MonoBehaviour
    {
        [SerializeField] private GameEvent _eventToSubscribe;
        [SerializeField] private UnityEvent _unityEventToRaise;

        private void Start()
        {
            _eventToSubscribe.Subscribe(_unityEventToRaise.Invoke, this.GetCancellationTokenOnDestroy());
        }
    }
}