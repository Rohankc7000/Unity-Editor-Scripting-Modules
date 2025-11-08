
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
}
