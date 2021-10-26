using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "PoseRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "PoseRequestResponseEvent")]
    public class PoseRequestResponseEvent : RequestResponseEvent<Pose>
    {
    }
}