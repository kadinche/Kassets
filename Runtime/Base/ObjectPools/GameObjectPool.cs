using UnityEngine;

namespace Kadinche.Kassets.ObjectPool
{
    [CreateAssetMenu(fileName = "NewTransformPool", menuName = MenuHelper.DefaultObjectPoolMenu + "TransformPool")]
    public class GameObjectPool : ObjectPoolCore<GameObject>
    {
        [SerializeField] private GameObject _targetPrefab;

        public Transform DefaultParent { get; set; } = null;
        
        protected override GameObject CreatePooledItem()
        {
            var go = Instantiate(_targetPrefab, DefaultParent);
            return go;
        }

        protected override void OnTakeFromPool(GameObject go) => go.SetActive(true);
        protected override void OnReturnedToPool(GameObject go) => go.SetActive(false);
        protected override void OnDestroyPoolObject(GameObject go) => Destroy(go);
    }
}