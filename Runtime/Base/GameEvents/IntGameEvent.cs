using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "IntEvent", menuName = MenuHelper.DefaultGameEventMenu + "IntEvent")]
    public class IntGameEvent : GameEvent<int>
    {
    }
}