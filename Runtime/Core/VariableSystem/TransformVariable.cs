using UnityEngine;

namespace Kassets.VariableSystem
{
    [CreateAssetMenu(fileName = "TransformVariable", menuName = MenuHelper.DefaultVariableMenu + "Transform")]
    public class TransformVariable : VariableSystemBase<Transform>
    {
    }
}