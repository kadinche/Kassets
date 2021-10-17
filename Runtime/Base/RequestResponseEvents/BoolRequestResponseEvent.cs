using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "BoolRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "BoolRequestResponseEvent")]
    public class BoolRequestResponseEvent : RequestResponseEvent<bool>
    {
    }
}