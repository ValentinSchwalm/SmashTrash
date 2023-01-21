using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitScene()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
