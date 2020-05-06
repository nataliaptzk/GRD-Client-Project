using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for changing scenes and quitting the game.
/// - Natalia Pietrzak
/// </summary>
public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}