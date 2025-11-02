using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AutoSaveEditor : EditorWindow
{
	private const string menuOption = "File/AutoSave";
	private static EditorWindow window;
	private int choice;
	private const string ONE_SECOND = "1 sec";
	private const string THIRTY_SECOND = "30 sec";
	private const string ONE_MINUTE = "1 min";
	private const string FIVE_MINUTE = "5 min";
	private string[] choices = { ONE_SECOND, THIRTY_SECOND, ONE_MINUTE, FIVE_MINUTE };
	private static float saveTime = 1;
	private static float nextSave = 0;

	public static bool IsEnabled
	{
		get { return EditorPrefs.GetBool(menuOption, false); }
		set { EditorPrefs.SetBool(menuOption, value); }
	}

	[InitializeOnLoadMethod]
	private static void InitializeAutoSave()
	{
		if (IsEnabled)
		{
			EditorApplication.update += AutoSaveUpdate;
		}
	}
	private static void AutoSaveUpdate()
	{
		if (!IsEnabled) return;

		if (EditorApplication.timeSinceStartup > nextSave)
		{
			string scenePath = EditorSceneManager.GetActiveScene().path;
			bool saved = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), scenePath);
			nextSave = (float)EditorApplication.timeSinceStartup + saveTime;
			//Debug.Log("Scene auto-saved: " + saved + " at " + System.DateTime.Now);
		}
	}

	[MenuItem(menuOption, false, 175)]
	public static void ToggleAutoSave()
	{
		IsEnabled = !IsEnabled;
		if (IsEnabled)
		{
			EditorApplication.update += AutoSaveUpdate;
			ShowWindow();
		}
		else
		{
			EditorApplication.update -= AutoSaveUpdate;
			CloseWindow();
		}
	}

	private static void CloseWindow()
	{
		if (window != null)
		{
			window.Close();
		}
	}

	private static void ShowWindow()
	{
		window = GetWindow<AutoSaveEditor>("Auto Save Settings");
		window.Show();
	}

	[MenuItem(menuOption, true)]
	private static bool ToggleActionValidate()
	{
		Menu.SetChecked(menuOption, IsEnabled);
		return true;
	}

	private void OnGUI()
	{
		EditorGUILayout.LabelField("Interval: ");
		EditorGUILayout.Space();
		EditorGUI.BeginChangeCheck();
		choice = EditorGUILayout.Popup(choice, choices);
		if (EditorGUI.EndChangeCheck())
		{
			switch (choices[choice])
			{
				case ONE_SECOND:
					saveTime = 1;
					break;
				case THIRTY_SECOND:
					saveTime = 30;
					break;
				case ONE_MINUTE:
					saveTime = 60;
					break;
				case FIVE_MINUTE:
					saveTime = 300;
					break;
			}
		}

		if (IsEnabled)
		{
			float timeToSave = nextSave - (float)EditorApplication.timeSinceStartup;
			if (EditorApplication.timeSinceStartup > nextSave)
			{
				string path = EditorSceneManager.GetActiveScene().path;
				bool saveSuccess = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), path);
				nextSave = (float)EditorApplication.timeSinceStartup + saveTime;
				//Debug.Log("Was saved successful?:   " + saveSuccess);
			}
		}
		Repaint();
	}
}
