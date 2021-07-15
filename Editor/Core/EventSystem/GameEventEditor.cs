using UnityEditor;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem
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
                e.Raise();
        }
    }
}