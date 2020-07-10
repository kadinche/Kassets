using UnityEngine;

namespace Kassets.VariableSystem
{
    [CreateAssetMenu(fileName = "GameObjectVariable", menuName = MenuHelper.DefaultVariableMenu + "GameObject")]
    public class GameObjectVariable : VariableSystemBase<GameObject>
    {
    }
}