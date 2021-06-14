using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.UnityComponents
{
    public class TransformToVariable : MonoBehaviour
    {
        [Tooltip("Attach current transform reference to a TransformVariable")]
        [SerializeField] private TransformVariable _transformVariable;

        private void Awake() => _transformVariable.Value = transform;
    }
}