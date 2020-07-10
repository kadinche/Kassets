using Cysharp.Threading.Tasks;
using Kassets.Utilities;
using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.UnityComponents
{
    public class EulerAngleBinder : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _eulerAngle;
        [SerializeField] private BindingType _bindingType;

        private void Start() => transform.BindEulerAngle(_eulerAngle, _bindingType, this.GetCancellationTokenOnDestroy());
    }
}