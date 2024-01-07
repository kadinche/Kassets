using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "QuaternionRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "QuaternionRequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use QuaternionTransaction instead.")]
    public class QuaternionRequestResponseEvent : RequestResponseEvent<Quaternion>
    {
    }
}