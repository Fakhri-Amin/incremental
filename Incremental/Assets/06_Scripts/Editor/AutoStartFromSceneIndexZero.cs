using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class AutoStartFromSceneIndexZero
{
    private const string MenuPath = "Tools/Auto Start From Scene 0";
    private const string PrefKey = "AutoStartFromSceneIndexZero_Enabled";

    static AutoStartFromSceneIndexZero()
    {
        // Subscribe to play mode state change
#pragma warning disable UDR0001 // Domain Reload Analyzer
        EditorApplication.playModeStateChanged += LoadFirstSceneOnPlay;
#pragma warning restore UDR0001 // Domain Reload Analyzer

        // Ensure menu checkmark matches saved preference
        Menu.SetChecked(MenuPath, IsEnabled);
    }

    private static bool IsEnabled
    {
        get => EditorPrefs.GetBool(PrefKey, true); // default ON
        set
        {
            EditorPrefs.SetBool(PrefKey, value);
            Menu.SetChecked(MenuPath, value);
        }
    }

    [MenuItem(MenuPath)]
    private static void ToggleEnabled()
    {
        IsEnabled = !IsEnabled;
    }

    private static void LoadFirstSceneOnPlay(PlayModeStateChange state)
    {
        if (!IsEnabled)
            return;

        if (state == PlayModeStateChange.ExitingEditMode)
        {
            if (EditorBuildSettings.scenes.Length == 0)
            {
                Debug.LogWarning("Build Settings contains no scenes.");
                return;
            }

            string firstScenePath = EditorBuildSettings.scenes[0].path;

            // Prompt to save current modified scenes before switching
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(firstScenePath);
            }
            else
            {
                EditorApplication.isPlaying = false; // Cancel entering play mode
            }
        }
        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            // Clear the override so Unity returns to editing the original scene
            EditorSceneManager.playModeStartScene = null;
        }
    }
}
