using Kadinche.Kassets.EventSystem;
using UniRx;
using UnityEngine;

namespace Kadinche.Kassets.ObjectPool.Samples
{
    public class ShootInputProvider : MonoBehaviour
    {
        [SerializeField] private GameEvent _shootBulletEvent;
        [SerializeField] private KeyCode _shootKey;

        private void Start()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(_shootKey))
                .Subscribe(_ => _shootBulletEvent.Raise())
                .AddTo(this);
        }
    }
}