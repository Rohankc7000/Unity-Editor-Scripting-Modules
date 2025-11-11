using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform), true)]
public class CustomInspectorScripting : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if (GUILayout.Button("Click Me"))
		{

		}
	}
}
