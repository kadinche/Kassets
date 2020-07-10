using UnityEditor;
using UnityEngine;

namespace Kassets.EventSystem
{
    [CustomEditor(typeof(GameEvent), true)]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var e = (GameEvent) target;
            if (GUILayout.Button("Raise"))
                e.RaiseEditor();
        }
    }
}