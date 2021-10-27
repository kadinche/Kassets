using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "PoseEvent", menuName = MenuHelper.DefaultGameEventMenu + "PoseEvent")]
    public class PoseGameEvent : GameEvent<Pose>
    {
    }
}