using UnityEngine;
using UnityEngine.UI;

public class PlayerOutput : MonoBehaviour
{
    /// <summary>
    /// Поле объекта основных действий персонажа
    /// </summary>
    PlayerParameters playerParameters;
    /// <summary>
    /// Поле изображения очков здоровья
    /// </summary>
    [SerializeField] Image HealthImage;
    /// <summary>
    /// Поле вывода счета игрока
    /// </summary>
    [SerializeField] Text point;

    private void Start()
    {
        // Получение объекта действий игрока
        playerParameters = GetComponent<PlayerParameters>();
        // Подписка на событие изменения здоровья персонажа
        playerParameters.player.HealthEvent += Player_HealthEvent;
    }

    /// <summary>
    /// Обработчик события изменения здоровья персонажа
    /// </summary>
    /// <param name="parameter"></param>
    private void Player_HealthEvent(float parameter)
    {
        // Изменение изображения очков здоровья персонажа
        HealthImage.fillAmount = parameter;
    }

    void Update()
    {
        // Вывод информации по счету игрока на экран
        point.text = $"Счет: {playerParameters.player.Points}. Лучший счет: {playerParameters.player.BestCount}";
    }
}
