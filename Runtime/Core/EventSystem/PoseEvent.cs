using UnityEngine;

namespace Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "PoseEvent", menuName = MenuHelper.DefaultEventMenu + "PoseEvent")]
    public class PoseEvent : GameEvent<Pose>
    {
    }
}