using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace Kadinche.Kassets.ObjectPool.Samples
{
    public class ShootInputProvider : MonoBehaviour
    {
        [SerializeField] private GameEvent _shootBulletEvent;
        [SerializeField] private KeyCode _shootKey;

        private void Update()
        {
            if (Input.GetKeyDown(_shootKey)) 
                _shootBulletEvent.Raise();
        }
    }
}