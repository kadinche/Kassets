using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kassets.EventSystem;
using UnityEngine;

namespace Kassets.UnityComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DMovement : MonoBehaviour
    {  
        [SerializeField] private Vector2Event _movementAxis;
        [SerializeField] protected float _moveSpeed = 10;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _activeAxis;

        protected virtual void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            
            _movementAxis.Subscribe(value => _activeAxis = value).AddTo(token);
            
            UniTaskAsyncEnumerable.EveryUpdate(PlayerLoopTiming.FixedUpdate).Subscribe(_ => Move(_activeAxis)).AddTo(token);
        }

        protected virtual void Move(Vector2 moveAxis)
        {
            _rigidbody2D.velocity = moveAxis * _moveSpeed;
        }
    }
}