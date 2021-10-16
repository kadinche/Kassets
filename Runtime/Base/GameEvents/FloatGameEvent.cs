using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "FloatEvent", menuName = MenuHelper.DefaultGameEventMenu + "FloatEvent")]
    public class FloatGameEvent : GameEvent<float>
    {
    }
}