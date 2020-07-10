using UnityEngine;

namespace Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "DoubleExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "DoubleExchangeEvent")]
    public class DoubleExchangeEvent : ExchangeEvent<double>
    {
    }
}