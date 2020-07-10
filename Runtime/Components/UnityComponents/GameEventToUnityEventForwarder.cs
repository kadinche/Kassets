using Cysharp.Threading.Tasks;
using Kassets.EventSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Kassets.UnityComponents
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