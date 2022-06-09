using UnityEngine;

namespace Kadinche.Kassets.ObjectPool
{
    [CreateAssetMenu(fileName = "NewTransformPool", menuName = MenuHelper.DefaultObjectPoolMenu + "TransformPool")]
    public class TransformPool : ObjectPoolCore<Transform>
    {
        [SerializeField] private GameObject _targetPrefab;

        public Transform DefaultParent { get; set; } = null;
        
        protected override Transform CreatePooledItem()
        {
            var go = Instantiate(_targetPrefab, DefaultParent);
            return go.transform;
        }

        protected override void OnTakeFromPool(Transform tr) => tr.gameObject.SetActive(true);
        protected override void OnReturnedToPool(Transform tr) => tr.gameObject.SetActive(false);
        protected override void OnDestroyPoolObject(Transform tr) => Destroy(tr.gameObject);
    }
}