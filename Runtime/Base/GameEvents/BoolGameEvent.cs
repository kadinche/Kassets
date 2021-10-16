using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "BoolEvent", menuName = MenuHelper.DefaultGameEventMenu + "BoolEvent")]
    public class BoolGameEvent : GameEvent<bool>
    {
        public void RaiseToggled() => Raise(!_value);
    }
}