using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoLinker : Editor
{
	static Dictionary<string, GameObject> hierarchyNameToGameobjectMap;
	static Dictionary<string, SerializedObject> inspectorFieldnameToSerializedPropertyMap;


	[InitializeOnLoadMethod]
	private static void Setup()
	{
		hierarchyNameToGameobjectMap = new Dictionary<string, GameObject>();
		inspectorFieldnameToSerializedPropertyMap = new Dictionary<string, SerializedObject>();
		SetupHirerachyMap();
		SetupInspectorMap();
		HandleAutoLinking();
	}

	private static void SetupHirerachyMap()
	{
		GameObject[] gameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject go in gameObjects)
		{
			string key = go.name.ToLower().Replace(" ", "");
			hierarchyNameToGameobjectMap.Add(key, go);
		}
	}

	private static void SetupInspectorMap()
	{
		foreach (GameObject go in hierarchyNameToGameobjectMap.Values)
		{
			Component[] components = go.GetComponents<Component>();
			foreach (Component co in components)
			{
				SerializedObject serializedObject = new SerializedObject(co);
				SerializedProperty serializedProperty = serializedObject.GetIterator();
				while (serializedProperty.NextVisible(true))
				{
					string key = serializedProperty.displayName.ToLower().Replace(" ", "");
					if (serializedProperty.propertyType == SerializedPropertyType.ObjectReference)
					{
						if (!inspectorFieldnameToSerializedPropertyMap.ContainsKey(key))
						{
							inspectorFieldnameToSerializedPropertyMap.Add(key, serializedObject);
						}
					}
				}
			}
		}
	}

	private static void HandleAutoLinking()
	{
		foreach (string item in inspectorFieldnameToSerializedPropertyMap.Keys)
		{
			string key = item.ToLower().Replace(" ", "");
			if (hierarchyNameToGameobjectMap.ContainsKey(key))
			{
				SerializedProperty serializedProperty = inspectorFieldnameToSerializedPropertyMap[key].FindProperty(key);
				serializedProperty.objectReferenceValue = hierarchyNameToGameobjectMap[key];
				serializedProperty.serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
