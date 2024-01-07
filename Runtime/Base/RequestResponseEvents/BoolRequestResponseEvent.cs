using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "BoolRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "BoolRequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use BoolTransaction instead.")]
    public class BoolRequestResponseEvent : RequestResponseEvent<bool>
    {
    }
}