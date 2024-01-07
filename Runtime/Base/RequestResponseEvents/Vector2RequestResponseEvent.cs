using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [CreateAssetMenu(fileName = "Vector2RequestResponseEvent", menuName = MenuHelper.DefaultRequestResponseEventMenu + "Vector2RequestResponseEvent")]
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use Vector2Transaction instead.")]
    public class Vector2RequestResponseEvent : RequestResponseEvent<Vector2>
    {
    }
}