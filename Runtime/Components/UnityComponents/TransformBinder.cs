using Cysharp.Threading.Tasks;
using Kadinche.Kassets.Utilities;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;

namespace Kadinche.Kassets.UnityComponents
{
    public class TransformBinder : MonoBehaviour
    {
        [SerializeField] private TransformVariable _transformVariable;
        [SerializeField] private TransformBindingType _bindingType;

        private void Start() => transform.BindTransform(_transformVariable, _bindingType, this.GetCancellationTokenOnDestroy());
    }
}