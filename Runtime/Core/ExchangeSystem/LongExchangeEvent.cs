using UnityEngine;

namespace Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "LongExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "LongExchangeEvent")]
    public class LongExchangeEvent : ExchangeEvent<long>
    {
    }
}