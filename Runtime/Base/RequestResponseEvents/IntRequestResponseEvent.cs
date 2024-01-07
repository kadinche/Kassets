using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "IntRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "IntRequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use IntTransaction instead.")]
    public class IntRequestResponseEvent : RequestResponseEvent<int>
    {
    }
}