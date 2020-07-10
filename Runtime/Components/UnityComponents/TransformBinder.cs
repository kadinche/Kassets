using Cysharp.Threading.Tasks;
using Kassets.Utilities;
using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.UnityComponents
{
    public class TransformBinder : MonoBehaviour
    {
        [SerializeField] private TransformVariable _transformVariable;
        [SerializeField] private TransformBindingType _bindingType;

        private void Start() => transform.BindTransform(_transformVariable, _bindingType, this.GetCancellationTokenOnDestroy());
    }
}