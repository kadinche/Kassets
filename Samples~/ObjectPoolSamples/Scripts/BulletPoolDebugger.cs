#if UNITY_2021_1_OR_NEWER

using TMPro;
using UnityEngine;

namespace Kadinche.Kassets.ObjectPool.Samples
{
    public class BulletPoolDebugger : MonoBehaviour
    {
        [SerializeField] private TransformPool _bulletTransformPool;
        [SerializeField] private TMP_Text _debugText;

        private void Update()
        {
            _debugText.text = $"Object{(_bulletTransformPool.CountInactive > 1 ? "s" : "")} in Pool: {_bulletTransformPool.CountInactive}";
        }
    }
}

#endif