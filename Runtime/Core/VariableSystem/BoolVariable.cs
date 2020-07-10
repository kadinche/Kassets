using UnityEngine;

namespace Kassets.VariableSystem
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = MenuHelper.DefaultVariableMenu + "Bool")]
    public class BoolVariable : VariableSystemBase<bool>
    {
        public void ToggleValue() => Value = !Value;
    }
}