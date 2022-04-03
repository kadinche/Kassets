using Kadinche.Kassets.Variable;
using UnityEngine;

namespace Kadinche.Kassets.Sample
{
    public class AddToPrefabTest : MonoBehaviour
    {
        [SerializeField] private IntVariable _someIntVariable;
        [SerializeField] private StringVariable _someStringVariable;
        [SerializeField] private SomeScriptableObject _someScriptableObject;
        
#if UNITY_EDITOR

        // Add
        [ContextMenu("Add Int Variable", false)]
        private void AddIntVariable() => _someIntVariable = gameObject.Add<IntVariable>();

        [ContextMenu("Add Int Variable", true)]
        private bool AddIntVariableValidate() => gameObject.AddValidate<IntVariable>();
        
        [ContextMenu("Add String Variable", false)]
        private void AddStringVariable() => _someStringVariable = gameObject.Add<StringVariable>();

        [ContextMenu("Add String Variable", true)]
        private bool AddStringVariableValidate() => gameObject.AddValidate<StringVariable>();

        [ContextMenu("Add SomeScriptableObject", false)]
        private void AddSomeScriptableObject() => _someScriptableObject = gameObject.Add<SomeScriptableObject>();

        [ContextMenu("Add SomeScriptableObject", true)]
        private bool AddSomeScriptableObjectValidate() => gameObject.AddValidate<SomeScriptableObject>();
        
        // Remove
        [ContextMenu("Remove Int Variable", false)]
        private void RemoveIntVariable()
        {
            gameObject.Remove<IntVariable>();
            _someIntVariable = null;
        }
        
        [ContextMenu("Remove Int Variable", true)]
        private bool RemoveIntVariableValidate() => gameObject.RemoveValidate<IntVariable>();
        
        [ContextMenu("Remove String Variable", false)]
        private void RemoveStringVariable()
        {
            gameObject.Remove<StringVariable>();
            _someStringVariable = null;
        }
        
        [ContextMenu("Remove String Variable", true)]
        private bool RemoveStringVariableValidate() => gameObject.RemoveValidate<StringVariable>();
        
        [ContextMenu("Remove SomeScriptableObject", false)]
        private void RemoveSomeScriptableObject()
        {
            gameObject.Remove<SomeScriptableObject>();
            _someScriptableObject = null;
        }
        
        [ContextMenu("Remove SomeScriptableObject", true)]
        private bool RemoveSomeScriptableObjectValidate() => gameObject.RemoveValidate<SomeScriptableObject>();

#endif
    }
}