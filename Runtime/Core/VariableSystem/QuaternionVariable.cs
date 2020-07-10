using UnityEngine;

namespace Kassets.VariableSystem
{
    [CreateAssetMenu(fileName = "QuaternionVariable", menuName = MenuHelper.DefaultVariableMenu + "Quaternion")]
    public class QuaternionVariable : VariableSystemBase<Quaternion>
    {
    }
}