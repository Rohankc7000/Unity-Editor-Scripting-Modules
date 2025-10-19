using System;
using UnityEditor;
using UnityEngine;


public class BatchRenameToolWindow : EditorWindow
{
	string bacthName = "";
	string batchNumber = "";
	bool showOptions = true;

	[MenuItem("Window/Batch Rename")]
	public static void ShowWindow()
	{
		EditorWindow window = GetWindow<BatchRenameToolWindow>("Batch Rename");
		window.maxSize = new Vector2(500, 150);
		window.minSize = window.maxSize;
		GUIContent guiContent = new GUIContent();
		guiContent.text = "Batch Rename";
		window.titleContent = guiContent;
		window.Show();
	}

	private void OnGUI()
	{
		GUILayout.Space(2);
		EditorGUILayout.LabelField("Step 1: Select objects in the hirerachy", EditorStyles.boldLabel);
		GUILayout.Space(2);

		GUIStyle guiStyle = new GUIStyle(EditorStyles.foldout);
		guiStyle.fontStyle = FontStyle.Bold;
		showOptions = EditorGUILayout.Foldout(showOptions, "Step 2: Enter rename info", guiStyle);

		if (showOptions)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("\tEnter name for the batch");
			bacthName = EditorGUILayout.TextField(bacthName);
			GUILayout.Space(2);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("\tEnter starting number");
			batchNumber = EditorGUILayout.TextField(batchNumber);
			GUILayout.Space(2);
			EditorGUILayout.EndHorizontal();
		}

		GUILayout.Space(2);
		EditorGUILayout.LabelField("Step 3: Click the rename button", EditorStyles.boldLabel);

		GUILayout.Space(2);
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(2);
		if (GUILayout.Button("Rename"))
		{
			try
			{
				int numberAsInt = int.Parse(batchNumber);

				foreach (GameObject go in Selection.objects)
				{
					go.name = $"{bacthName}_{numberAsInt}";
					numberAsInt++;
				}
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}
		GUILayout.Space(2);
		GUILayout.EndHorizontal();

		Repaint();
	}

}
