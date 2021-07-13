using UnityEngine;

namespace Kadinche.Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "PoseExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "ExchangeEvent")]
    public class PoseExchangeEvent : ExchangeEvent<Pose>
    {
    }
}