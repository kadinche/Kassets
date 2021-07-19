using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "ByteRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "ByteRequestResponseEvent")]
    public class ByteRequestResponseEvent : RequestResponseEvent<byte>
    {
    }
}