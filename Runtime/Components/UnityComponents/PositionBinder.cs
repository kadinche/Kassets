using Cysharp.Threading.Tasks;
using Kassets.Utilities;
using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.UnityComponents
{
    public class PositionBinder : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _position;
        [SerializeField] private BindingType _bindingType;
        
        private void Start() => transform.BindPosition(_position, _bindingType, this.GetCancellationTokenOnDestroy());
    }
}