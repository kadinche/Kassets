using UnityEngine;

namespace Kadinche.Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "ExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "ByteExchangeEvent")]
    public class ByteExchangeEvent : ExchangeEvent<byte>
    {
    }
}