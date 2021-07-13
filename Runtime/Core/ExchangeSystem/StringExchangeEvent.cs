using UnityEngine;

namespace Kadinche.Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "StringExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "StringExchangeEvent")]
    public class StringExchangeEvent : ExchangeEvent<string>
    {
    }
}