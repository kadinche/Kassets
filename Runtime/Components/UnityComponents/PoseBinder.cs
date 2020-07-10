using Cysharp.Threading.Tasks;
using Kassets.Utilities;
using Kassets.VariableSystem;
using UnityEngine;

namespace Kassets.UnityComponents
{
    public class PoseBinder : MonoBehaviour
    {
        [SerializeField] private PoseVariable _pose;
        [SerializeField] private BindingType _positionBindingType;
        [SerializeField] private BindingType _rotationBindingType;
        
        private void Start() => transform.BindPose(_pose, _positionBindingType, _rotationBindingType, this.GetCancellationTokenOnDestroy());
    }
}