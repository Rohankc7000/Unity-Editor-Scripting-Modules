using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class ProjectOrganizerWindow : EditorWindow
{
	private int selectedTabIndex = 0;
	private string[] tabs = { "Organizer", "Asset Type Mappings" };
	private int countOfAssetTypeRows;
	private List<AssetTypeRow> assetTypeRows;
	private int totalNumberOfFileExtensions;
	private bool isDirty = false;
	private string[] assetTypeNames;

	private int countOfOrganizerRows;
	private List<OrganizerRow> orgainzerRows;

	class OrganizerRow
	{
		public int SelectedOptionIndex;
		public string FolderPath;
		public Object Obj;
	}

	class AssetTypeRow
	{
		public string Name;
		public string FileExtension;
	}

	private Dictionary<string, List<string>> assetTypes = new Dictionary<string, List<string>>
	{
		{"Prefabs",new List<string>(){".prefab"}},
		{"Animations",new List<string>(){".anim"}},
		{"Images",new List<string>(){".png",".jpeg"}},
	};

	[MenuItem("Window/Project Oraganizer Tool")]
	public static void ShowWindow()
	{
		EditorWindow window = GetWindow<ProjectOrganizerWindow>("Project Oraganizer Tool");
		window.Show();
	}

	private void Awake()
	{
		InitalizeFields();
	}

	private void InitalizeFields()
	{
		foreach (string key in assetTypes.Keys)
		{
			totalNumberOfFileExtensions += assetTypes[key].Count;
		}
		countOfAssetTypeRows = totalNumberOfFileExtensions;
		assetTypeRows = new List<AssetTypeRow>();
		assetTypeNames = new string[totalNumberOfFileExtensions];
		assetTypes.Keys.CopyTo(assetTypeNames, 0);

		for (int i = 0; i < totalNumberOfFileExtensions; i++)
		{
			string key = assetTypeNames[i];
			if (key != null)
			{
				int numberOfFileExtensionsForAssetType = assetTypes[key].Count;
				for (int j = 0; j < numberOfFileExtensionsForAssetType; j++)
				{
					assetTypeRows.Add(new AssetTypeRow()
					{
						Name = assetTypeNames[i],
						FileExtension = assetTypes[assetTypeNames[i]][j],
					});
				}
			}
		}

		countOfOrganizerRows = assetTypes.Keys.Count;
		orgainzerRows = new List<OrganizerRow>();
		for (int i = 0; i < countOfOrganizerRows; i++)
		{
			orgainzerRows.Add(new OrganizerRow
			{
				SelectedOptionIndex = i,
				FolderPath = "Assets/" + assetTypeNames[i],
			});
		}
	}

	private void OnGUI()
	{
		DrawToolBarTabs();
		EditorGUILayout.Space(20);
		if (selectedTabIndex == 0)
		{
			if (isDirty)
			{
				isDirty = false;
				UpdateAssetTypes(assetTypeNames.Length);
			}
			for (int i = 0; i < countOfOrganizerRows; i++)
			{
				DrawOrganizerRow(i);
			}
		}
		else
		{
			for (int i = 0; i < countOfAssetTypeRows; i++)
			{
				DrawAssetTypeRow(i);
			}
		}
	}

	private void DrawOrganizerRow(int currentIndex)
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.Space();

		GUILayout.BeginVertical();
		EditorGUILayout.LabelField("Asset Type");
		EditorGUI.BeginChangeCheck();
		orgainzerRows[currentIndex].SelectedOptionIndex = EditorGUILayout.Popup("",
			orgainzerRows[currentIndex].SelectedOptionIndex, assetTypeNames
		);
		if (EditorGUI.EndChangeCheck())
		{
			orgainzerRows[currentIndex].FolderPath = "Assets/" + assetTypeNames[orgainzerRows[currentIndex].SelectedOptionIndex];
		}
		GUILayout.EndVertical();
		EditorGUILayout.Space();

		GUILayout.BeginVertical();
		EditorGUILayout.LabelField("Path to Folder");
		orgainzerRows[currentIndex].FolderPath = EditorGUILayout.TextField(orgainzerRows[currentIndex].FolderPath);
		GUILayout.EndVertical();
		EditorGUILayout.Space();

		GUILayout.BeginVertical();
		EditorGUILayout.LabelField("Select Folder");
		EditorGUI.BeginChangeCheck();
		orgainzerRows[currentIndex].Obj = EditorGUILayout.ObjectField(
			orgainzerRows[currentIndex].Obj,
			typeof(UnityEditor.DefaultAsset),
			true
			);
		if (EditorGUI.EndChangeCheck())
		{
			orgainzerRows[currentIndex].FolderPath = "Assets/" + orgainzerRows[currentIndex].Obj.name;
		}
		GUILayout.EndVertical();
		EditorGUILayout.Space();

		GUILayout.EndHorizontal();
		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
	}

	private void UpdateAssetTypes(int currentIndex)
	{
		assetTypes.Add(assetTypeRows[currentIndex].Name, new List<string>());
		assetTypes[assetTypeRows[currentIndex].Name].Add(assetTypeRows[currentIndex].FileExtension);
		totalNumberOfFileExtensions = 0;
		foreach (string key in assetTypes.Keys)
		{
			totalNumberOfFileExtensions += assetTypes[key].Count;
		}
		assetTypeNames = new string[totalNumberOfFileExtensions - 1];
		assetTypes.Keys.CopyTo(assetTypeNames, 0);
	}

	private void DrawToolBarTabs()
	{
		GUILayout.BeginHorizontal();
		selectedTabIndex = GUILayout.Toolbar(selectedTabIndex, tabs);
		GUILayout.EndHorizontal();
	}

	private void DrawAssetTypeRow(int currentIndex)
	{
		GUILayout.BeginHorizontal();

		EditorGUILayout.Space();
		GUILayout.BeginVertical();
		EditorGUILayout.LabelField("Name");
		EditorGUI.BeginChangeCheck();
		if (assetTypeRows != null)
		{
			assetTypeRows[currentIndex].Name = EditorGUILayout.TextField(assetTypeRows[currentIndex].Name);
		}
		if (EditorGUI.EndChangeCheck())
		{
			isDirty = true;
		}
		GUILayout.EndVertical();

		EditorGUILayout.Space();
		GUILayout.BeginVertical();
		EditorGUILayout.LabelField("FileExtension");
		EditorGUI.BeginChangeCheck();
		if (assetTypeRows != null)
		{
			assetTypeRows[currentIndex].FileExtension = EditorGUILayout.TextField(assetTypeRows[currentIndex].FileExtension);
		}
		if (EditorGUI.EndChangeCheck() && assetTypes.ContainsKey(assetTypeRows[currentIndex].Name))
		{
			isDirty = true;
		}
		GUILayout.EndVertical();
		EditorGUILayout.Space();
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
	}
}
