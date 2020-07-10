using UnityEngine;

namespace Kassets.VariableSystem
{
    [CreateAssetMenu(fileName = "ColorVariable", menuName = MenuHelper.DefaultVariableMenu + "Color")]
    public class ColorVariable : VariableSystemBase<Color>
    {
    }
}