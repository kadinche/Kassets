using UnityEngine;

namespace Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "FloatEvent", menuName = MenuHelper.DefaultEventMenu + "FloatEvent")]
    public class FloatEvent : GameEvent<float>
    {
    }
}