using UnityEngine;

namespace Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "Vector3Event", menuName = MenuHelper.DefaultEventMenu + "Vector3Event")]
    public class Vector3Event : GameEvent<Vector3>
    {
    }
}