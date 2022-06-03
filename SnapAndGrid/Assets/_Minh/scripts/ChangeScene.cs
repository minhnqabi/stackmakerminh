#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;


public class ChangeScene : Editor
{

    [MenuItem("Open Scene/Editor LV")]
    public static void OpenEditor()
    {
        OpenScene("TestEditor3");
    }

    [MenuItem("Open Scene/Game")]
    public static void OpenGame()
    {
        OpenScene("Game");
    }



    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
        }
    }
}
#endif