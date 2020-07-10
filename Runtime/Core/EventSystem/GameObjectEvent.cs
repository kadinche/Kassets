using UnityEngine;

namespace Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "GameObjectEvent", menuName = MenuHelper.DefaultEventMenu + "GameObjectEvent")]
    public class GameObjectEvent : GameEvent<GameObject>
    {
    }
}