using Cysharp.Threading.Tasks;
using Kassets.EventSystem;
using UnityEngine;
using VirtualHandshake.Utilities;

namespace Kassets.Components
{
    public class RecenterCommandBinder : MonoBehaviour
    {
        [SerializeField] private GameEventCollection recenterCommands;

        private void Start() => recenterCommands.SubscribeAnyEvent(XRDeviceUtility.Recenter, this.GetCancellationTokenOnDestroy());
    }
}