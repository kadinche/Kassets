using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "PoseRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "RequestResponseEvent")]
    public class PoseRequestResponseEvent : RequestResponseEvent<Pose>
    {
    }
}