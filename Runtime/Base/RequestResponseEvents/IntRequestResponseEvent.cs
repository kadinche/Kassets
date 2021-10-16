using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "IntRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "IntRequestResponseEvent")]
    public class IntRequestResponseEvent : RequestResponseEvent<int>
    {
    }
}