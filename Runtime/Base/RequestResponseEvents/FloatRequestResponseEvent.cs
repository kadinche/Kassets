using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "FloatRequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "FloatRequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use FloatTransaction instead.")]
    public class FloatRequestResponseEvent : RequestResponseEvent<float>
    {
    }
}