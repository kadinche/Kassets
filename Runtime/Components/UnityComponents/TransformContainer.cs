using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.UnityComponents
{
    public class TransformContainer : MonoBehaviour
    {
        [SerializeField] private TransformVariable _transformVariable;
        [SerializeField] private bool _destroyObjectOnDestroy;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            _transformVariable.Subscribe(HandleTransform, token);
        }

        private void HandleTransform(Transform tr)
        {
            if (tr != null)
                _transformVariable.Value.SetParent(transform, false);
        }

        private void OnDestroy()
        {
            if (!_destroyObjectOnDestroy)
                // release parent so target won't get destroyed along with parent
                _transformVariable.Value.SetParent(null);
            else
                // target will be destroyed along the parent, hence set to null.
                _transformVariable.Value = null;
        }
    }
}