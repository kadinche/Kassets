using UnityEditor;
using UnityEngine;

namespace Kadinche.Kassets.CommandSystem
{
    [CustomEditor(typeof(CommandCore), true)]
    public class CommandEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var e = (CommandCore) target;
            if (GUILayout.Button("Execute"))
                e.Execute();
        }
    }
}