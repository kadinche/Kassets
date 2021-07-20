using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "QuaternionEvent", menuName = MenuHelper.DefaultEventMenu + "QuaternionEvent")]
    public class QuaternionEvent : GameEvent<Quaternion>
    {
    }
}