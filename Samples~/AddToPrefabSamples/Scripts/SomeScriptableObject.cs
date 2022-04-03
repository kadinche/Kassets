using UnityEngine;

namespace Kadinche.Kassets.Sample
{
    [CreateAssetMenu(fileName = "SomeScriptableObject", menuName = MenuHelper.DefaultOtherMenu + "SomeScriptableObject", order = 100)]
    public class SomeScriptableObject : ScriptableObject
    {
        public int id;
        public string someName;
    }
}