using UnityEditor;
using UnityEngine;

public class TestEditorWindowCallbacks : EditorWindow
{
	[MenuItem("Window/Editor Script Lifecycle")]
	public static void ShowWindow()
	{
		EditorWindow editorWindow = GetWindow(typeof(TestEditorWindowCallbacks));
		editorWindow.Show();
	}

	private void Awake()
	{
		Debug.Log("Awake() called");
	}

	private void OnEnable()
	{
		Debug.Log("OnEnable() called");
	}

	private void OnFocus()
	{
		Debug.Log("OnFocus() called");
	}

	private void OnLostFocus()
	{
		Debug.Log("OnLostFocus() called");
	}

	private void OnHierarchyChange()
	{
		Debug.Log("OnHierarchyChange() called");
	}

	private void OnProjectChange()
	{
		Debug.Log("OnProjectChange() called");
	}

	private void OnSelectionChange()
	{
		Debug.Log("OnSelectionChange() called");
	}

	private void OnInspectorUpdate()
	{
		Debug.Log("OnInspectorUpdate() called");
	}

	private void Update()
	{
		Debug.Log("Update() called");
	}

	private void OnGUI()
	{
		Debug.Log("OnGUI() called");

		GUILayout.Label("EditorWindow Callback Tester", EditorStyles.boldLabel);

		if (GUILayout.Button("Repaint"))
		{
			Debug.Log("Repaint() triggered");
			Repaint();
		}

		if (GUILayout.Button("Close Window"))
		{
			Debug.Log("Close() triggered");
			Close();
		}
	}

	private void OnDisable()
	{
		Debug.Log("OnDisable() called");
	}

	private void OnDestroy()
	{
		Debug.Log("OnDestroy() called");
	}
}
