using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "PoseRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "PoseRequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use PoseTransaction instead.")]
    public class PoseRequestResponseEvent : RequestResponseEvent<Pose>
    {
    }
}