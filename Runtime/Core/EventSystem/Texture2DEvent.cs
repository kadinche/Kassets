using UnityEngine;

namespace Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "Texture2DEvent", menuName = MenuHelper.DefaultEventMenu + "Texture2DEvent")]
    public class Texture2DEvent : GameEvent<Texture2D>
    {
    }
}