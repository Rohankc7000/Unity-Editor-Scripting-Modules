using UnityEngine;
using UnityEditor;

public class MissingReferenceDetector : EditorWindow
{
	[MenuItem("Custom Tools/Find Missing References")]
	public static void ShowWindow()
	{
		EditorWindow window = GetWindow<MissingReferenceDetector>("Find Missing References");
		window.maxSize = new Vector2(250, 100);
		window.minSize = window.maxSize;

	}

	private void OnGUI()
	{
		EditorGUILayout.Space();
		if (GUILayout.Button("Find Missing References", EditorStyles.boldLabel))
		{
			GameObject[] gameobjects = FindObjectsOfType<GameObject>();
			foreach (GameObject go in gameobjects)
			{
				Component[] components = go.GetComponents<Component>();
				foreach (Component component in components)
				{
					SerializedObject serialzedObject = new SerializedObject(component);
					SerializedProperty serializedProperty = serialzedObject.GetIterator();
					while (serializedProperty.NextVisible(true))
					{
						if (serializedProperty.propertyType == SerializedPropertyType.ObjectReference)
						{
							if (serializedProperty.objectReferenceValue == null)
							{
								Debug.Log($"<color=red><b>Missing reference: </b></color> {serializedProperty.displayName} on {go.name}", go);
							}
						}
					}
				}
			}
		}
		Repaint();
	}
}
