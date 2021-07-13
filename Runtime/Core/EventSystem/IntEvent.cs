using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "IntEvent", menuName = MenuHelper.DefaultEventMenu + "IntEvent")]
    public class IntEvent : GameEvent<int>
    {
    }
}