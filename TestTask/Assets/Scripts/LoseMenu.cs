using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
    /// <summary>
    /// Поле компонента аниматор
    /// </summary>
    Animation animation;

    /// <summary>
    /// Вывод результата на экран поражения
    /// </summary>
    [SerializeField] Text countText;

    /// <summary>
    /// Метод запуска меню поражения
    /// </summary>
    /// <param name="player"></param>
    public void ShowLoseMenu(PlayerModel player)
    {
        animation = GetComponent<Animation>();
        animation.Play();
        countText.text = $"Текущий счет: {player.Points}, лучший счет: {player.BestCount}";
    }
}
