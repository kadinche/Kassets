using UnityEngine;

namespace Kadinche.Kassets.Variable
{
    [CreateAssetMenu(fileName = "GameObjectVariable", menuName = MenuHelper.DefaultVariableMenu + "GameObject")]
    public class GameObjectVariable : VariableCore<GameObject>
    {
    }
}