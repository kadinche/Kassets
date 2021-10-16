using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "Texture2DEvent", menuName = MenuHelper.DefaultGameEventMenu + "Texture2DEvent")]
    public class Texture2DGameEvent : GameEvent<Texture2D>
    {
    }
}