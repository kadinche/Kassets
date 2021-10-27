using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "Vector3Event", menuName = MenuHelper.DefaultGameEventMenu + "Vector3Event")]
    public class Vector3GameEvent : GameEvent<Vector3>
    {
    }
}