using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "DoubleRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "DoubleRequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use DoubleTransaction instead.")]
    public class DoubleRequestResponseEvent : RequestResponseEvent<double>
    {
    }
}