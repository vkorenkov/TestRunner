using System.ComponentModel;

public class PlayerModel
{
    /// <summary>
    /// Делегат событий персонажа
    /// </summary>
    /// <param name="parameter"></param>
    public delegate void EventDelegate(float parameter);
    /// <summary>
    /// Событие увеличения темпа игры
    /// </summary>
    public event EventDelegate ShiftSpeedEvent;
    /// <summary>
    /// Событие изменения очков здоровья персонажа
    /// </summary>
    public event EventDelegate HealthEvent;

    private int health;
    /// <summary>
    /// Свойство здоровья персонажа
    /// </summary>
    public int Health
    {
        get => health; 
        set { health = value; CheckHealthCount(); }
    }

    private int points;
    /// <summary>
    /// Свойство собранных очков персонажа
    /// </summary>
    public int Points
    {
        get => points;
        set { points = value; CheckCoinsCount(); CheckPoints(); }
    }

    /// <summary>
    /// Свойство лучшего счета игрока
    /// </summary>
    public int BestCount { get; set; }

    /// <summary>
    /// Конструктор создания персонажа
    /// </summary>
    /// <param name="health"></param>
    /// <param name="points"></param>
    public PlayerModel(int health, int points)
    {
        Health = health;
        Points = points;
    }

    public PlayerModel()
    {
    }

    /// <summary>
    /// Метод проверки количества очков игрока
    /// </summary>
    void CheckCoinsCount()
    {
        switch (Points)
        {
            case 100:
                ShiftSpeedEvent?.Invoke(20);
                break;
            case 200:
                ShiftSpeedEvent?.Invoke(22);
                break;
            case 300:
                ShiftSpeedEvent?.Invoke(24);
                break;
            case 400:
                ShiftSpeedEvent?.Invoke(28);
                break;
            case 500:
                ShiftSpeedEvent?.Invoke(32);
                break;
            case 1000:
                ShiftSpeedEvent?.Invoke(35);
                break;
        }
    }

    /// <summary>
    /// Метод проверки количества очков здоровья персонажа
    /// </summary>
    void CheckHealthCount()
    {
        switch (Health)
        {
            case 2:
                HealthEvent?.Invoke(0.67f);
                break;
            case 1:
                HealthEvent?.Invoke(0.34f);
                break;
            case 0:
                HealthEvent?.Invoke(0);
                break;
        }
    }

    /// <summary>
    /// Метод присваивания лучшего счета игрока
    /// </summary>
    void CheckPoints()
    {
        if(BestCount < Points)
            BestCount = Points;
    }
}
