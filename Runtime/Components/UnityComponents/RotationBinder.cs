using Cysharp.Threading.Tasks;
using Kadinche.Kassets.Utilities;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;

namespace Kadinche.Kassets.UnityComponents
{
    public class RotationBinder : MonoBehaviour
    {
        [SerializeField] private QuaternionVariable _rotation;
        [SerializeField] private BindingType _bindingType;
        
        private void Start() => transform.BindRotation(_rotation, _bindingType, this.GetCancellationTokenOnDestroy());
    }
}