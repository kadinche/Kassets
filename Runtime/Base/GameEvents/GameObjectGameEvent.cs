using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "GameObjectEvent", menuName = MenuHelper.DefaultGameEventMenu + "GameObjectEvent")]
    public class GameObjectGameEvent : GameEvent<GameObject>
    {
    }
}