using UnityEngine;

namespace Kadinche.Kassets.Variable
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = MenuHelper.DefaultVariableMenu + "Bool")]
    public class BoolVariable : VariableBase<bool>
    {
        public void ToggleValue() => Value = !Value;
    }
}