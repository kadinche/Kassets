using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "StringRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "StringRequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use StringTransaction instead.")]
    public class StringRequestResponseEvent : RequestResponseEvent<string>
    {
    }
}