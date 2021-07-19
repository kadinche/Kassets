using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "QuaternionRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "QuaternionRequestResponseEvent")]
    public class QuaternionRequestResponseEvent : RequestResponseEvent<Quaternion>
    {
    }
}