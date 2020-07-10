using Cysharp.Threading.Tasks;
using Kassets.Utilities;
using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.UnityComponents
{
    public class RotationBinder : MonoBehaviour
    {
        [SerializeField] private QuaternionVariable _rotation;
        [SerializeField] private BindingType _bindingType;
        
        private void Start() => transform.BindRotation(_rotation, _bindingType, this.GetCancellationTokenOnDestroy());
    }
}