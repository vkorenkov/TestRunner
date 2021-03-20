using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    /// <summary>
    /// Метод загрузки игровой сцены
    /// </summary>
    public void StartGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Метод выхода из приложения
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
