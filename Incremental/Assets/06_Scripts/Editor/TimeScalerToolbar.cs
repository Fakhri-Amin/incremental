// #if UNITY_EDITOR
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.UIElements;
// using System.Reflection;

// [InitializeOnLoad]
// public static class TimeScalerToolbar
// {
//     private static VisualElement toolbarUI;
//     private static readonly float[] speedLevels = { 1f, 2f, 5f }; // speeds to cycle through
//     private static int currentIndex = 0;
//     private static float positionOffset = 2f; // offset to place next to SceneSwitcher
//     private static float buttonHeight = 20f;

//     static TimeScalerToolbar()
//     {
//         EditorApplication.delayCall += AddToolbarUI;
//         EditorApplication.playModeStateChanged += OnPlayModeChanged;
//     }

//     static void AddToolbarUI()
//     {
//         var toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
//         if (toolbarType == null) return;

//         var toolbars = Resources.FindObjectsOfTypeAll(toolbarType);
//         if (toolbars.Length == 0) return;

//         var toolbar = toolbars[0];
//         var rootField = toolbarType.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
//         if (rootField == null) return;

//         var root = rootField.GetValue(toolbar) as VisualElement;
//         if (root == null) return;

//         var leftContainer = root.Q("ToolbarZoneLeftAlign");
//         if (leftContainer == null) return;

//         if (toolbarUI != null)
//         {
//             leftContainer.Remove(toolbarUI);
//         }

//         toolbarUI = new IMGUIContainer(OnGUI);
//         toolbarUI.style.marginLeft = positionOffset;
//         leftContainer.Add(toolbarUI);
//     }

//     static void OnGUI()
//     {
//         bool isPlaying = EditorApplication.isPlaying;

//         EditorGUI.BeginDisabledGroup(!isPlaying);
//         GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
//         {
//             fixedHeight = buttonHeight,
//             fontStyle = FontStyle.Bold
//         };

//         string buttonLabel = $"Speed x{speedLevels[currentIndex]}";
//         if (GUILayout.Button(buttonLabel, buttonStyle, GUILayout.Width(90), GUILayout.Height(buttonHeight)))
//         {
//             CycleTimeScale();
//         }

//         EditorGUI.EndDisabledGroup();
//     }

//     static void CycleTimeScale()
//     {
//         // Move to next speed index
//         currentIndex = (currentIndex + 1) % speedLevels.Length;
//         Time.timeScale = speedLevels[currentIndex];

//         Debug.Log($"[TimeScalerToolbar] Time scale set to x{speedLevels[currentIndex]}");
//     }

//     static void OnPlayModeChanged(PlayModeStateChange state)
//     {
//         if (state == PlayModeStateChange.ExitingPlayMode)
//         {
//             // Reset time scale when exiting Play Mode
//             Time.timeScale = 1f;
//             currentIndex = 0;
//         }

//         if (state == PlayModeStateChange.EnteredPlayMode || state == PlayModeStateChange.ExitingPlayMode)
//         {
//             EditorApplication.delayCall += AddToolbarUI;
//         }
//     }
// }
// #endif