using Cysharp.Threading.Tasks;
using Kadinche.Kassets.Utilities;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;

namespace Kadinche.Kassets.UnityComponents
{
    public class EulerAngleBinder : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _eulerAngle;
        [SerializeField] private BindingType _bindingType;

        private void Start() => transform.BindEulerAngle(_eulerAngle, _bindingType, this.GetCancellationTokenOnDestroy());
    }
}