using UnityEditor;
using UnityEngine;

namespace Kadinche.Kassets.CommandSystem
{
    [CustomEditor(typeof(CommandBase), true)]
    public class CommandEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var e = (CommandBase) target;
            if (GUILayout.Button("Execute"))
                e.Execute();
        }
    }
}