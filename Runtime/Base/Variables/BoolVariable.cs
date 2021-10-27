using UnityEngine;

namespace Kadinche.Kassets.Variable
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = MenuHelper.DefaultVariableMenu + "Bool")]
    public class BoolVariable : VariableCore<bool>
    {
        public void ToggleValue() => Value = !Value;
    }
}