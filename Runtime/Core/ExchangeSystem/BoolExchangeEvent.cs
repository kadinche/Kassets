using UnityEngine;

namespace Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "BoolExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "BoolExchangeEvent")]
    public class BoolExchangeEvent : ExchangeEvent<bool>
    {
    }
}