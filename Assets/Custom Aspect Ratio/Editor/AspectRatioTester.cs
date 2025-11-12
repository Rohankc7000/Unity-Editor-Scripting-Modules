using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;


[InitializeOnLoad]
public class AspectRatioTester : Editor
{
	private static string applicationDataPath = Application.dataPath + "/";
	private static int numberOfScreenshotsSaved = 0;
	private static bool startedScreenshot = false;
	private static bool menuOptionWasClicked = false;

	private static List<string> aspectRatios = new List<string>()
	{
		applicationDataPath + "Free Aspect.png",
		applicationDataPath + "16x9 Aspect.png",
		applicationDataPath + "16x10 Aspect.png",
		applicationDataPath + "Full HD(1920 x 1080).png",
	};

	[MenuItem("Custom Tools/Test Aspect Ratio")]
	private static void TestAspectRatios()
	{
		menuOptionWasClicked = true;
		startedScreenshot = false;
		numberOfScreenshotsSaved = 0;
	}

	static AspectRatioTester()
	{
		EditorApplication.update += EditorToolLoop;
	}

	private static void EditorToolLoop()
	{
		if (menuOptionWasClicked && !startedScreenshot)
		{
			startedScreenshot = true;
			SaveScreenshotAtAspectRatio(numberOfScreenshotsSaved, aspectRatios[numberOfScreenshotsSaved]);
		}

		if (numberOfScreenshotsSaved < 4 && System.IO.File.Exists(aspectRatios[numberOfScreenshotsSaved]))
		{
			numberOfScreenshotsSaved++;
			startedScreenshot = false;
			Refresh();
		}

		if (numberOfScreenshotsSaved == 4)
		{
			menuOptionWasClicked = false;
		}
	}

	private static void Refresh()
	{
		AssetDatabase.Refresh();
	}

	private static void SaveScreenshotAtAspectRatio(int index, string fileName)
	{
		SetSize(index);
		TakeScreenShot(fileName);
	}

	public static void SetSize(int index)
	{
		var gameViewWindowType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
		var gameViewWindow = EditorWindow.GetWindow(gameViewWindowType);
		var SizeSelectionCallback = gameViewWindowType.GetMethod("SizeSelectionCallback", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		SizeSelectionCallback.Invoke(gameViewWindow, new object[] { index, null });
	}

	public static void TakeScreenShot(string fileName)
	{
		ScreenCapture.CaptureScreenshot(fileName);
	}
}
