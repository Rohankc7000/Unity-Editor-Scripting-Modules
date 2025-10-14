using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class CustomHirerachyOptions
{
	static CustomHirerachyOptions()
	{
		EditorApplication.hierarchyWindowItemOnGUI += hirerachyWindowItemOnGUI;
	}

	private static void hirerachyWindowItemOnGUI(int instanceID, Rect selectionRect)
	{
		Debug.Log("Editor script code called....");
	}
}
