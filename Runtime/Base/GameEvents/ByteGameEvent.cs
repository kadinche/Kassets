using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "ByteEvent", menuName = MenuHelper.DefaultGameEventMenu + "ByteEvent")]
    public class ByteGameEvent : GameEvent<byte>
    {
    }
}