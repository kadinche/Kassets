using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kassets.EventSystem;
using Kassets.VariableSystem;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Kassets.UnityComponents
{
    internal enum AxisMap
    {
        None,
        XAxis,
        YAxis,
        ZAxis,
        XInverted,
        YInverted,
        ZInverted
    }
    public class TransformRotation : MonoBehaviour
    {
#if ODIN_INSPECTOR
        [InfoBox("Target transform to rotate. If null or empty, set current transform as target.")]
#else
        [Tooltip("Target transform to rotate. If null or empty, set current transform as target.")]
#endif
        [SerializeField] private TransformVariable _rotateTarget;
        
        [SerializeField] private Vector2Event _rotationAxis;
        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private AxisMap xAxisMap;
        [SerializeField] private AxisMap yAxisMap;

        private void Start() => _rotationAxis.Skip(1).Subscribe(Rotate).AddTo(this.GetCancellationTokenOnDestroy());

        private void Rotate(Vector2 rotationAxis)
        {
            var target = _rotateTarget == null || _rotateTarget.Value == null ? transform : _rotateTarget.Value;

            var euler = GetEuler(target, rotationAxis);

            target.rotation = Quaternion.Euler(euler);
        }

        private Vector3 GetEuler(Transform target, Vector2 rotationAxis)
        {
            var euler = target.rotation.eulerAngles;

            var dx = rotationAxis.x * _rotationSpeed * Time.deltaTime;
            var dy = rotationAxis.y * _rotationSpeed * Time.deltaTime;
            
            switch (xAxisMap)
            {
                case AxisMap.XAxis:
                    euler.x += dx;
                    break;
                case AxisMap.YAxis:
                    euler.y += dx;
                    break;
                case AxisMap.ZAxis:
                    euler.z += dx;
                    break;
                case AxisMap.XInverted:
                    euler.x -= dx;
                    break;
                case AxisMap.YInverted:
                    euler.y -= dx;
                    break;
                case AxisMap.ZInverted:
                    euler.z -= dx;
                    break;
            }

            switch (yAxisMap)
            {
                case AxisMap.XAxis:
                    euler.x += dy;
                    break;
                case AxisMap.YAxis:
                    euler.y += dy;
                    break;
                case AxisMap.ZAxis:
                    euler.z += dy;
                    break;
                case AxisMap.XInverted:
                    euler.x -= dy;
                    break;
                case AxisMap.YInverted:
                    euler.y -= dy;
                    break;
                case AxisMap.ZInverted:
                    euler.z -= dy;
                    break;
            }

            return euler;
        }
    }
}