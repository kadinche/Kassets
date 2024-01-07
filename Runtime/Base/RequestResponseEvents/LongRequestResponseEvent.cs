using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "LongRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "LongRequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use LongTransaction instead.")]
    public class LongRequestResponseEvent : RequestResponseEvent<long>
    {
    }
}