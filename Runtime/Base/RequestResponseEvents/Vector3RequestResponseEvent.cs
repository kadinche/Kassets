using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "Vector3RequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "Vector3RequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use Vector3Transaction instead.")]
    public class Vector3RequestResponseEvent : RequestResponseEvent<Vector3>
    {
    }
}