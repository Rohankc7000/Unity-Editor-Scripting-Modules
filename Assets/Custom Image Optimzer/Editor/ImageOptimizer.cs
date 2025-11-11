using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(TextureImporter), true)]
public class ImageOptimizer : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if (GUILayout.Button("Optimize"))
		{
			string path = AssetDatabase.GetAssetPath(Selection.activeObject);
			TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);
			int width;
			int height;
			importer.GetSourceTextureWidthAndHeight(out width, out height);
			int maxDimensionSize = Math.Max(width, height);
			TextureImporterSettings textureImporterSettings = new TextureImporterSettings();
			importer.ReadTextureSettings(textureImporterSettings);
			textureImporterSettings.maxTextureSize = (int)Math.Pow(2, (int)Math.Log(maxDimensionSize - 1, 2) + 1);
			importer.SetTextureSettings(textureImporterSettings);
			EditorUtility.SetDirty(importer);
			importer.SaveAndReimport();
		}
	}
}
