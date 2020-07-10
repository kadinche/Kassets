using UnityEngine;

namespace Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "QuaternionExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "QuaternionExchangeEvent")]
    public class QuaternionExchangeEvent : ExchangeEvent<Quaternion>
    {
    }
}