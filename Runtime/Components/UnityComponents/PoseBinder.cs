using Cysharp.Threading.Tasks;
using Kadinche.Kassets.Utilities;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;

namespace Kadinche.Kassets.UnityComponents
{
    public class PoseBinder : MonoBehaviour
    {
        [SerializeField] private PoseVariable _pose;
        [SerializeField] private BindingType _positionBindingType;
        [SerializeField] private BindingType _rotationBindingType;
        
        private void Start() => transform.BindPose(_pose, _positionBindingType, _rotationBindingType, this.GetCancellationTokenOnDestroy());
    }
}