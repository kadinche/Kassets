using Cysharp.Threading.Tasks;
using Kadinche.Kassets.Utilities;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;

namespace Kadinche.Kassets.UnityComponents
{
    public class PositionBinder : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _position;
        [SerializeField] private BindingType _bindingType;
        
        private void Start() => transform.BindPosition(_position, _bindingType, this.GetCancellationTokenOnDestroy());
    }
}