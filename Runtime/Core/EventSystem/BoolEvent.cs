using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    [CreateAssetMenu(fileName = "BoolEvent", menuName = MenuHelper.DefaultEventMenu + "BoolEvent")]
    public class BoolEvent : GameEvent<bool>
    {
        public void RaiseToggled() => Raise(!inspectorRaiseValue);
    }
}