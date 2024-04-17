using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides functionality for menu operation such as loading scene, starting the game,
/// and exiting the application. This is attached to a menu object which handles the UI.
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>
    /// Loads a new scene.
    /// </summary>
    /// <param name="sceneToLoad"></param>
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    /// <summary>
    /// To restart the game.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// To exit the application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}

