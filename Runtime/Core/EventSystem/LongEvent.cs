using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "LongEvent", menuName = MenuHelper.DefaultEventMenu + "LongEvent")]
    public class LongEvent : GameEvent<long>
    {
    }
}