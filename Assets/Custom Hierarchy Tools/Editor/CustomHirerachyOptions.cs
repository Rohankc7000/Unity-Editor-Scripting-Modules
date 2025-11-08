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

	private static string gameobjectName;
	public static bool IsFavorited
	{
		get { return EditorPrefs.GetBool("favorite" + gameobjectName, false); }
		set { EditorPrefs.SetBool("favorite" + gameobjectName, value); }
	}

	private static void hirerachyWindowItemOnGUI(int instanceID, Rect selectionRect)
	{
		GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
		if (gameObject != null)
		{
			gameobjectName = gameObject.name;
			//Debug.Log(IsFavorited);

			DrawActiveToggleButton(instanceID, selectionRect);
			AddInfoScriptToGameObject(instanceID);
			DrawInfoButton(instanceID, selectionRect, "");
			DrawZoomInButton(instanceID, selectionRect, "Click to zoom the object");
			DrawPrefabButton(instanceID, selectionRect, "Save as prefab");
			DrawDeleteGameObjectButton(instanceID, selectionRect, "Delete gameobject");
			DrawFavoriteButton(instanceID, selectionRect, "Add to favorites");
		}
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

	private static void DrawButtonWithTexture(float x, float y, float size, string imageName, Action action, GameObject go, string tooltip)
	{
		if (go)
		{
			GUIStyle guiStyle = new GUIStyle();
			guiStyle.fixedHeight = 0;
			guiStyle.fixedWidth = 0;
			guiStyle.stretchHeight = true;
			guiStyle.stretchWidth = true;

			Rect r = DrawRect(x, y, size);
			Texture t = Resources.Load<Texture>(imageName);
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

	private static void AddInfoScriptToGameObject(int id)
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


	#region ------------------ For Zooming the GameObject ------------------

	private static void DrawZoomInButton(int id, Rect rect, string tooltip)
	{
		GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;
		if (go)
		{
			DrawButtonWithTexture(rect.x + 175, rect.y + 3, 14, "zoomIn", () =>
			{
				Selection.activeGameObject = go;
				SceneView.FrameLastActiveSceneView();
			}, go, tooltip);
		}
	}

	#endregion ---------------------------------------------------------------



	#region ------------------ For Creating the prefab ------------------

	private static void DrawPrefabButton(int id, Rect rect, string tooltip)
	{
		GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;
		if (go)
		{
			DrawButtonWithTexture(rect.x + 198, rect.y, 18, "prefab", () =>
			{
				const string pathToPrefabsFolder = "Assets/Prefabs";
				bool prefabFolderExist = AssetDatabase.IsValidFolder(pathToPrefabsFolder);
				if (!prefabFolderExist)
				{
					AssetDatabase.CreateFolder("Assets", "Prefabs");
				}
				string prefabName = go.name + ".prefab";
				string prefabPath = pathToPrefabsFolder + "/" + prefabName;
				AssetDatabase.DeleteAsset(prefabPath);
				GameObject prefab = PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
				EditorGUIUtility.PingObject(prefab);
			}, go, tooltip);
		}
	}

	#endregion ---------------------------------------------------------------

	#region ------------------ Delete the gameobject ------------------

	private static void DrawDeleteGameObjectButton(int id, Rect rect, string tooltip)
	{
		GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;
		if (go)
		{
			DrawButtonWithTexture(rect.x + 220, rect.y, 15, "delete", () =>
			{
				Selection.activeGameObject = go;
				UnityEngine.Object.DestroyImmediate(go);
			}, go, tooltip);
		}
	}

	#endregion ---------------------------------------------------------------

	#region ------------------ Favorite System ------------------

	private static void DrawFavoriteButton(int id, Rect rect, string tooltip)
	{
		GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;
		if (gameObject)
		{

			if (IsFavorited)
			{
				DrawButtonWithTexture(rect.x + 125, rect.y + 3, 10, "favorite_filled", () =>
				{
				}, gameObject, tooltip);
			}
			else
			{
				DrawButtonWithTexture(rect.x + 125, rect.y + 3, 10, "favorite_outline",
					() =>
					{
						IsFavorited = !IsFavorited;
						FavoriteMenu.AddToFavorites(gameObject);
					}, gameObject, tooltip);
			}
		}
	}

	#endregion ---------------------------------------------------------------


	#region ------------------ Region Name ------------------

	#endregion ---------------------------------------------------------------


	#region ------------------ Region Name ------------------

	#endregion ---------------------------------------------------------------


	#region ------------------ Region Name ------------------

	#endregion ---------------------------------------------------------------
}