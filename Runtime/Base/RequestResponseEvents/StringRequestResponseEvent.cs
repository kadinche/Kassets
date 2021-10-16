using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "StringRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "StringRequestResponseEvent")]
    public class StringRequestResponseEvent : RequestResponseEvent<string>
    {
    }
}