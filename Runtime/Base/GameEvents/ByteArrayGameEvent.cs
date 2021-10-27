using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "ByteArrayEvent", menuName = MenuHelper.DefaultGameEventMenu + "ByteArrayEvent")]
    public class ByteArrayGameEvent : GameEvent<byte[]>
    {
    }
}