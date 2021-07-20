using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "ByteArrayEvent", menuName = MenuHelper.DefaultEventMenu + "ByteArrayEvent")]
    public class ByteArrayEvent : GameEvent<byte[]>
    {
    }
}