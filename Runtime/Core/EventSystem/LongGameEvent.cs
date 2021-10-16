using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "LongEvent", menuName = MenuHelper.DefaultGameEventMenu + "LongEvent")]
    public class LongGameEvent : GameEvent<long>
    {
    }
}