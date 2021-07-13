using UnityEngine;

namespace Kadinche.Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "QuaternionExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "QuaternionExchangeEvent")]
    public class QuaternionExchangeEvent : ExchangeEvent<Quaternion>
    {
    }
}