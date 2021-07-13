using UnityEngine;

namespace Kadinche.Kassets.VariableSystem
{
    [CreateAssetMenu(fileName = "GameObjectVariable", menuName = MenuHelper.DefaultVariableMenu + "GameObject")]
    public class GameObjectVariable : VariableSystemBase<GameObject>
    {
    }
}