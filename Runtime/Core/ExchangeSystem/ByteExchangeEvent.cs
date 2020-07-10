using UnityEngine;

namespace Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "ExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "ByteExchangeEvent")]
    public class ByteExchangeEvent : ExchangeEvent<byte>
    {
    }
}