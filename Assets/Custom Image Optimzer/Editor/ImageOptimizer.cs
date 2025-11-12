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
			textureImporterSettings.maxTextureSize = Mathf.NextPowerOfTwo(Mathf.Max(1, maxDimensionSize));
			importer.SetTextureSettings(textureImporterSettings);
			EditorUtility.SetDirty(importer);
			importer.SaveAndReimport();
		}
	}
}
