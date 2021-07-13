using Cysharp.Threading.Tasks;
using Kadinche.Kassets.EventSystem;
using UnityEngine;
using VirtualHandshake.Utilities;

namespace Kadinche.Kassets.Components
{
    public class RecenterCommandBinder : MonoBehaviour
    {
        [SerializeField] private GameEventCollection recenterCommands;

        private void Start() => recenterCommands.SubscribeAnyEvent(XRDeviceUtility.Recenter, this.GetCancellationTokenOnDestroy());
    }
}