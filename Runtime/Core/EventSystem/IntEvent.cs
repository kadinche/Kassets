using UnityEngine;

namespace Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "IntEvent", menuName = MenuHelper.DefaultEventMenu + "IntEvent")]
    public class IntEvent : GameEvent<int>
    {
    }
}