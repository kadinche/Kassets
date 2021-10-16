using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "DoubleEvent", menuName = MenuHelper.DefaultGameEventMenu + "DoubleEvent")]
    public class DoubleGameEvent : GameEvent<double>
    {
    }
}