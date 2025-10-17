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
		DrawActiveToggleButton(instanceID, selectionRect);
	}

	private static Rect DrawRect(float x, float y, float size)
	{
		return new Rect(x, y, size, size);
	}

	private static void DrawButtonWithToggle(int id, float x, float y, float size)
	{
		GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;
		if (go)
		{
			Rect r = DrawRect(x, y, size);
			go.SetActive(GUI.Toggle(r, go.activeSelf, string.Empty));

		}
	}

	private static void DrawActiveToggleButton(int id, Rect rect)
	{
		DrawButtonWithToggle(id, rect.x - 20, rect.y + 3, 10);
	}
}
