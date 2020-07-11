using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine.UI;

/// <summary>
/// Class that has misc functions that Unity doesn't provide
/// </summary>
public static class Utilities
{
    /*
     * Add any new scenes here for ease of access in the editor. Follow previous entries' format
     */
#if UNITY_EDITOR

    // Main menu
    [MenuItem("Scenes/Main Menu")] public static void MainMenuScene() => 
        OpenScene("Assets/Scenes/MainMenu.unity");

    // Game
    [MenuItem("Scenes/Game")] public static void ScenarioOne() => 
        OpenScene("Assets/Scenes/Game.unity");
    
    [MenuItem("Scenes/Add or Edit Scene Menu Options")] public static void EditMenu() =>
        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath("Assets/Scripts/Utility/Utilities.cs", typeof(Object)), 56);
    private static void OpenScene(string scenePath)
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene(scenePath);
    }
#endif
}