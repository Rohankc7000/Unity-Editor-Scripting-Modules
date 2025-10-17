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
		AddInfoScriptToGameObject(instanceID);
		DrawInfoButton(instanceID, selectionRect, "");
	}

	#region ---------- For Active and Inactive of Gameobjects

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

	#endregion ----------------------------------------------


	#region ---------- For Icons Gameobjects

	static void DrawButtonWithTexture(float x, float y, float size, string name, Action action, GameObject go, string tooltip)
	{
		if (go)
		{
			GUIStyle guiStyle = new GUIStyle();
			guiStyle.fixedHeight = 0;
			guiStyle.fixedWidth = 0;
			guiStyle.stretchHeight = true;
			guiStyle.stretchWidth = true;

			Rect r = DrawRect(x, y, size);
			Texture t = Resources.Load<Texture>(name);
			GUIContent gUIContent = new GUIContent();
			gUIContent.text = "";
			gUIContent.image = t;
			gUIContent.tooltip = tooltip;

			bool isClicked = GUI.Button(r, gUIContent, guiStyle);
			if (isClicked)
			{
				action?.Invoke();
			}
		}
	}
	private static void DrawInfoButton(int id, Rect rect, string tooltip)
	{
		GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;
		if (go)
		{
			Info infoScriptComponent = go.GetComponent<Info>();
			if (infoScriptComponent != null)
			{
				tooltip = infoScriptComponent.tooltip;
			}
		}
		DrawButtonWithTexture(rect.x + 150, rect.y + 2, 14, "info", () => { }, go, tooltip);
	}

	static void AddInfoScriptToGameObject(int id)
	{
		GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;
		if (go)
		{
			Info info = go.GetComponent<Info>();
			if (info == null)
			{
				go.AddComponent<Info>();
			}
		}
	}

	#endregion ----------------------------------------------

}