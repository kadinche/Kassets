using UnityEngine;

namespace Kadinche.Kassets.Collection
{
    [CreateAssetMenu(fileName = "PoseCollection", menuName = MenuHelper.DefaultCollectionMenu + "PoseCollection")]
    public class PoseCollection : Collection<Pose>
    {
    }
}