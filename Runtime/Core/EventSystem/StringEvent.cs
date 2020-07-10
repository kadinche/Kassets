using UnityEngine;

namespace Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "StringEvent", menuName = MenuHelper.DefaultEventMenu + "StringEvent")]
    public class StringEvent : GameEvent<string>
    {
    }
}