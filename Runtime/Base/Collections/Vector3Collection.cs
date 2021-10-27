using UnityEngine;

namespace Kadinche.Kassets.Collection
{
    [CreateAssetMenu(fileName = "Vector3Collection", menuName = MenuHelper.DefaultCollectionMenu + "Vector3Collection")]
    public class Vector3Collection : Collection<Vector2>
    {
    }
}