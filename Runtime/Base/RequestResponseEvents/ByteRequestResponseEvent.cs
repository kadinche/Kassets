using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "ByteRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "ByteRequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use ByteTransaction instead.")]
    public class ByteRequestResponseEvent : RequestResponseEvent<byte>
    {
    }
}