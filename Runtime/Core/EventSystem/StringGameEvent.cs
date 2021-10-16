using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "StringEvent", menuName = MenuHelper.DefaultGameEventMenu + "StringEvent")]
    public class StringGameEvent : GameEvent<string>
    {
    }
}