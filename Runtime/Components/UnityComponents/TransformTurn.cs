using System.Threading;
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
    internal enum TurnAngle
    {
        Free,
        Turn30,
        Turn45,
        Turn90,
    }
    public class TransformTurn : MonoBehaviour
    {
#if ODIN_INSPECTOR
        [InfoBox("Target transform to rotate. If null or empty, set current transform as target.")]
#else
        [Tooltip("Target transform to rotate. If null or empty, set current transform as target.")]
#endif
        [SerializeField] private TransformVariable _turnTarget;
        
        [SerializeField] private Vector2Event _turnAxis;
        [SerializeField] private TurnAngle _turnAngle;
        [Range(0, 1)]
        [SerializeField] private float _turnTreshold;

        private async void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            await _turnAxis.ForEachAwaitWithCancellationAsync(async (vector2, cancellationToken) => await Rotate(vector2, cancellationToken), token);
        }

        private async UniTask Rotate(Vector2 rotationAxis, CancellationToken token)
        {
            if (_turnAngle != TurnAngle.Free)
                if (!await UniTask.WaitUntil(() => Mathf.Abs(rotationAxis.x) > _turnTreshold, cancellationToken: token)
                    .SuppressCancellationThrow())
                    return;

            var target = _turnTarget == null || _turnTarget.Value == null ? transform : _turnTarget.Value;
            var euler = target.rotation.eulerAngles;
                euler.y += GetTurnAngle(rotationAxis.x);
            target.rotation = Quaternion.Euler(euler);

            if (_turnAngle != TurnAngle.Free)
                await UniTask.WaitUntil(() => Mathf.Approximately(rotationAxis.x, 0), cancellationToken: token).SuppressCancellationThrow();
        }

        private float GetTurnAngle(float xAxis)
        {
            var sign = xAxis > 0 ? 1 : -1;
            var value = 0f;
            switch (_turnAngle)
            {
                case TurnAngle.Free:
                    value = _turnTreshold * 10 * Time.deltaTime;
                    break;
                case TurnAngle.Turn30:
                    value = 30f;
                    break;
                case TurnAngle.Turn45:
                    value = 45f;
                    break;
                case TurnAngle.Turn90:
                    value = 90f;
                    break;
            }

            return value * sign;
        }
    }
}