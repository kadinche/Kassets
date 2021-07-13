using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "GameObjectEvent", menuName = MenuHelper.DefaultEventMenu + "GameObjectEvent")]
    public class GameObjectEvent : GameEvent<GameObject>
    {
    }
}