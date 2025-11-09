
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FavoriteMenu : EditorWindow
{
	private const string pathToFavoriteFolder = "Assets/Favorites";
	private static List<GameObject> favoritedObjects = new List<GameObject>();

	public static void AddToFavorites(GameObject gameObject)
	{
		bool doesFavoritesFolderExist = AssetDatabase.IsValidFolder(pathToFavoriteFolder);
		if (!doesFavoritesFolderExist)
		{
			AssetDatabase.CreateFolder("Assets", "Favorites");
		}
		string prefabName = gameObject.name + ".prefab";
		string prefabPath = pathToFavoriteFolder + "/" + prefabName;
		AssetDatabase.DeleteAsset(prefabName);
		GameObject prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath);
		favoritedObjects.Add(prefab);
	}

	private static void HandleFavoriteItemClicked(int index)
	{
		GameObject gameObject = favoritedObjects[index];
		GameObject cloneOfGameObject = PrefabUtility.InstantiatePrefab(gameObject) as GameObject;
		Camera currentSceneViewCamera = SceneView.lastActiveSceneView.camera;
		Vector3 position = currentSceneViewCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f));
		cloneOfGameObject.transform.position = position;
		Selection.activeGameObject = cloneOfGameObject;
		SceneView currentSceneView = SceneView.lastActiveSceneView;
		currentSceneView.AlignViewToObject(cloneOfGameObject.transform);
		EditorGUIUtility.PingObject(cloneOfGameObject);
	}

	[MenuItem("Favorites/Favorite Object 1", false, 50)]
	public static void HandleFavoriteItem1Clicked()
	{
		HandleFavoriteItemClicked(0);
	}

	[MenuItem("Favorites/Favorite Object 1", true)]
	public static bool HandleFavoriteItem1Option()
	{
		return favoritedObjects.Count >= 1;
	}

	[MenuItem("Favorites/Favorite Object 2", false, 50)]
	public static void HandleFavoriteItem2Clicked()
	{
		HandleFavoriteItemClicked(1);
	}

	[MenuItem("Favorites/Favorite Object 2", true)]
	public static bool HandleFavoriteItem2Option()
	{
		return favoritedObjects.Count >= 2;
	}

	[MenuItem("Favorites/Favorite Object 3", false, 50)]
	public static void HandleFavoriteItem3Clicked()
	{
		HandleFavoriteItemClicked(2);
	}

	[MenuItem("Favorites/Favorite Object 3", true)]
	public static bool HandleFavoriteItem3Option()
	{
		return favoritedObjects.Count >= 3;
	}

	[InitializeOnLoadMethod]
	private static void Setup()
	{
		favoritedObjects.Clear();
	}

	[MenuItem("Favorites/Clear Favorites", false, 100)]
	public static void ClearFavoriteMenu()
	{
		favoritedObjects.Clear();
		EditorPrefs.DeleteAll();
	}

}
