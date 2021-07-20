using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "ByteEvent", menuName = MenuHelper.DefaultEventMenu + "ByteEvent")]
    public class ByteEvent : GameEvent<byte>
    {
    }
}