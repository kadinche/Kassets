// Developed by Tom Kail at Inkle
// Released under the MIT Licence as held at https://opensource.org/licenses/MIT
// Original code: https://gist.github.com/tomkail/ba4136e6aa990f4dc94e0d39ec6a058c

// Must be placed within a folder named "Editor"
using System;
using System.Collections.Generic;
using Kadinche.Kassets;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Extends how ScriptableObject object references are displayed in the inspector
/// Shows you all values under the object reference
/// Also provides a button to create a new ScriptableObject if property is null.
/// </summary>
[CustomPropertyDrawer(typeof(KassetsCore), true)]
public class ExtendedScriptableObjectDrawer : PropertyDrawer
{
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		var totalHeight = EditorGUIUtility.singleLineHeight;
        if(property.objectReferenceValue == null || !AreAnySubPropertiesVisible(property)){
            return totalHeight;
        }
		if(property.isExpanded) {
			if (!(property.objectReferenceValue is ScriptableObject data)) 
				return EditorGUIUtility.singleLineHeight;
			
			using var serializedObject = new SerializedObject(data);
			using var prop = serializedObject.GetIterator();
			if (prop.NextVisible(true))
			{
				do {
					if (prop.name == "m_Script") continue;
					if (prop.name == "_value" && prop.propertyType == SerializedPropertyType.Generic && !prop.isArray) 
						totalHeight -= EditorGUIUtility.singleLineHeight;

					var subProp = serializedObject.FindProperty(prop.name);
					var height = EditorGUI.GetPropertyHeight(subProp, null, true) + EditorGUIUtility.standardVerticalSpacing;
					totalHeight += height;
				}
				while (prop.NextVisible(false));
			}
			// Add a tiny bit of height if open for the background
			totalHeight += EditorGUIUtility.standardVerticalSpacing;
		}
		return totalHeight;
	}

	private const int ButtonWidth = 66;

	private static readonly List<string> IgnoreClassFullNames = new() { "TMPro.TMP_FontAsset" };
	private static readonly List<string> IgnoreField = new() { "m_Script", "_value", "instanceSettings" };
	
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginProperty (position, label, property);
		var type = GetFieldType();
		
		if(type == null || IgnoreClassFullNames.Contains(type.FullName)) {
			EditorGUI.PropertyField(position, property, label);	
			EditorGUI.EndProperty ();
			return;
		}
		
		ScriptableObject propertySO = null;
		if (!property.hasMultipleDifferentValues && 
		    property.serializedObject.targetObject != null &&
		    property.serializedObject.targetObject is ScriptableObject scriptableObject)
		{
			propertySO = scriptableObject;
		}

		var guiContent = new GUIContent(property.displayName);
		var foldoutRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
		if (property.objectReferenceValue != null && AreAnySubPropertiesVisible(property))
		{
			property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, guiContent, true);
		} 
		else
		{
			// So yeah having a foldout look like a label is a weird hack 
			// but both code paths seem to need to be a foldout or 
			// the object field control goes weird when the codepath changes.
			// I guess because foldout is an interactable control of its own and throws off the controlID?
			foldoutRect.x += 12;
			EditorGUI.Foldout(foldoutRect, property.isExpanded, guiContent, true, EditorStyles.label);
		}
		var indentedPosition = EditorGUI.IndentedRect(position);
		var indentOffset = indentedPosition.x - position.x;
		var propertyRect = new Rect(position.x + (EditorGUIUtility.labelWidth - indentOffset), position.y, position.width - (EditorGUIUtility.labelWidth - indentOffset), EditorGUIUtility.singleLineHeight);

		if (propertySO != null || property.objectReferenceValue == null) 
			propertyRect.width -= ButtonWidth;

		EditorGUI.ObjectField(propertyRect, property, type, GUIContent.none);
		
		if (GUI.changed) 
			property.serializedObject.ApplyModifiedProperties();

		var buttonRect = new Rect(position.x + position.width - ButtonWidth, position.y, ButtonWidth, EditorGUIUtility.singleLineHeight);
			
		if (property.propertyType == SerializedPropertyType.ObjectReference && 
		    property.objectReferenceValue != null)
		{
			var data = (ScriptableObject)property.objectReferenceValue;
			
			if (property.isExpanded) {
				// Draw a background that shows us clearly which fields are part of the ScriptableObject
				GUI.Box(new Rect(0, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing - 1, Screen.width, position.height - EditorGUIUtility.singleLineHeight - EditorGUIUtility.standardVerticalSpacing), "");

				EditorGUI.indentLevel++;
				using var serializedObject = new SerializedObject(data);

				var y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
				
				// draw generic values
				using var value = serializedObject.FindProperty("_value");
				
				if (value != null)
				{
					if (value.propertyType == SerializedPropertyType.Generic && !value.isArray)
					{
						foreach (var child in value.GetChildren())
						{
							var height = EditorGUI.GetPropertyHeight(child, new GUIContent(child.displayName), true);
							EditorGUI.PropertyField(new Rect(position.x, y, position.width, height), child, true);
							y += height + EditorGUIUtility.standardVerticalSpacing;
						}
					}
					else
					{
						var height = EditorGUI.GetPropertyHeight(value, new GUIContent(value.displayName), true);
						EditorGUI.PropertyField(new Rect(position.x, y, position.width, height), value, true);
						y += height + EditorGUIUtility.standardVerticalSpacing;
					}
				}

				// Iterate over all the values
				using var prop = serializedObject.GetIterator();
				if (prop.NextVisible(true))
				{
					do {
						// Don't bother drawing the class file
						if (IgnoreField.Contains(prop.name)) continue;
						
						var height = EditorGUI.GetPropertyHeight(prop, new GUIContent(prop.displayName), true);
						EditorGUI.PropertyField(new Rect(position.x, y, position.width, height), prop, true);
						y += height + EditorGUIUtility.standardVerticalSpacing;
					}
					while (prop.NextVisible(false));
				}
				
				using var instanceSettings = serializedObject.FindProperty("instanceSettings");
				{
					var height = EditorGUI.GetPropertyHeight(instanceSettings, new GUIContent(instanceSettings.displayName), true);
					EditorGUI.PropertyField(new Rect(position.x, y, position.width, height), instanceSettings, true);
				}

				if (GUI.changed)
					serializedObject.ApplyModifiedProperties();

				EditorGUI.indentLevel--;
			}
		} 
		else
		{
			if (GUI.Button(buttonRect, "Create"))
			{
				var selectedAssetPath = "Assets";
				if (property.serializedObject.targetObject is MonoBehaviour monoBehaviour)
				{
					var ms = MonoScript.FromMonoBehaviour(monoBehaviour);
					selectedAssetPath = System.IO.Path.GetDirectoryName(AssetDatabase.GetAssetPath( ms ));
				}
				
				property.objectReferenceValue = CreateAssetWithSavePrompt(type, selectedAssetPath);
			}
		}
		property.serializedObject.ApplyModifiedProperties();
		EditorGUI.EndProperty();
	}

	// Creates a new ScriptableObject via the default Save File panel
	private static ScriptableObject CreateAssetWithSavePrompt (Type type, string path) {
		path = EditorUtility.SaveFilePanelInProject("Save ScriptableObject", type.Name+".asset", "asset", "Enter a file name for the ScriptableObject.", path);
		if (path == "") return null;
		var asset = ScriptableObject.CreateInstance(type);
		AssetDatabase.CreateAsset (asset, path);
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
		AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		EditorGUIUtility.PingObject(asset);
		return asset;
	}

	private Type GetFieldType () {
		var type = fieldInfo.FieldType;
		if(type.IsArray) type = type.GetElementType();
		else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)) type = type.GetGenericArguments()[0];
		return type;
	}

	private static bool AreAnySubPropertiesVisible(SerializedProperty property) {
        var data = (ScriptableObject)property.objectReferenceValue;
        using var serializedObject = new SerializedObject(data);
        using var prop = serializedObject.GetIterator();
        while (prop.NextVisible(true)) {
            if (prop.name == "m_Script") continue;
            return true; //if theres any visible property other than m_script
        }
        return false;
    }
}