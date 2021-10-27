using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "LongRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "LongRequestResponseEvent")]
    public class LongRequestResponseEvent : RequestResponseEvent<long>
    {
    }
}