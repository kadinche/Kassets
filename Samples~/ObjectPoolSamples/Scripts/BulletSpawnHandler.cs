#if UNITY_2021_1_OR_NEWER

using System;
using System.Collections;
using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace Kadinche.Kassets.ObjectPool.Samples
{
    public class BulletSpawnHandler : MonoBehaviour
    {
        [SerializeField] private GameEvent _spawnEvent;
        [SerializeField] private TransformPool _bulletTransformPool;
        [SerializeField] private float _bulletMaxDistance = 10f;
        [SerializeField] private float _bulletSpeed = 1f;
        
        private IDisposable _subscription;

        private void Start()
        {
            _bulletTransformPool.DefaultParent = transform;
            _subscription = _spawnEvent.Subscribe(Spawn);
        }

        private void Spawn()
        {
            StartCoroutine(SpawnAndMoveRoutine());
        }

        private IEnumerator SpawnAndMoveRoutine()
        {
            using var _ = _bulletTransformPool.Get(out var bullet);

            var startingPos = bullet.position;
            while (Vector3.Distance(bullet.position, startingPos) < _bulletMaxDistance)
            {
                bullet.position += bullet.forward * (_bulletSpeed * Time.deltaTime);
                yield return null;
            }

            bullet.position = startingPos;
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}

#endif