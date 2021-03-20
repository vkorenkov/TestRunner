using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Поле звукового сопроводждения
    /// </summary>
    SoundControl soundControl;

    private void Start()
    {
        // Установка времени на стандартную скорость
        if (Time.timeScale < 1)
            Time.timeScale = 1;

        // Получение компонента звукового сопровождения
        soundControl = GetComponent<SoundControl>();
        // Вызов метода запуска темы главного меню
        soundControl.PlaySound("mainmenutheme");
    }
}
