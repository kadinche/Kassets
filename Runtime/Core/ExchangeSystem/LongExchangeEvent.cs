using UnityEngine;

namespace Kadinche.Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "LongExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "LongExchangeEvent")]
    public class LongExchangeEvent : ExchangeEvent<long>
    {
    }
}