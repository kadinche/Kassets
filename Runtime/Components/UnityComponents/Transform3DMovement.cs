using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kadinche.Kassets.EventSystem;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Kadinche.Kassets.UnityComponents
{
    public class Transform3DMovement : MonoBehaviour
    {
#if ODIN_INSPECTOR
        [InfoBox("Target transform to move. If null or empty, set current transform as target.")]
#else
        [Tooltip("Target transform to move. If null or empty, set current transform as target.")]
#endif
        [SerializeField] private TransformVariable _moveTarget;
        
        [SerializeField] private Vector2Event _movementAxis;
        [SerializeField] private float _moveSpeed = 10;

        private Vector2 _activeAxis;

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            
            _movementAxis.Subscribe(value => _activeAxis = value).AddTo(token);
            
            UniTaskAsyncEnumerable.EveryUpdate().Subscribe(_ => Move(_activeAxis)).AddTo(token);
        }

        private void Move(Vector2 moveAxis)
        {
            var target = _moveTarget == null || _moveTarget.Value == null ? transform : _moveTarget.Value;

            var distance = GetDistance(target, moveAxis) * _moveSpeed * Time.deltaTime;

            target.position += distance;
        }
        
        protected virtual Vector3 GetDistance(Transform target, Vector2 moveAxis) => target.forward * moveAxis.y + target.right * moveAxis.x;
    }
}