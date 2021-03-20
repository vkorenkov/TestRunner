using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    /// <summary>
    /// Поле экземпляра класса меню поражения
    /// </summary>
    [SerializeField] LoseMenu loseMenu;
    /// <summary>
    /// Поле объекта сохранения/чтения лучшего счета
    /// </summary>
    SavePoints savePoints;
    /// <summary>
    /// Поле экземпляра класса игрока
    /// </summary>
    public PlayerModel player;
    /// <summary>
    /// Поле экземпляра класса движения игрока
    /// </summary>
    movePlayerScript move;
    /// <summary>
    /// Поле стандартного таймера после получения урона
    /// </summary>
    [SerializeField, Range(0, 3)] float defaultHitTime;
    /// <summary>
    /// Поле таймера после получения урона
    /// </summary>
    float hitTime;
    /// <summary>
    /// Временная скорость передвижения после получения урона
    /// </summary>
    float tempMoveForce;
    /// <summary>
    /// Поле обозначающее был ли получен урон
    /// </summary>
    bool isHit;
    /// <summary>
    /// Поле звукового оформления
    /// </summary>
    SoundControl soundControl;

    public PlayerParameters()
    {
        savePoints = new SavePoints();
        player = new PlayerModel(3, 0);
        defaultHitTime = 0.5f;
    }

    void Start()
    {
        // Получение необходимых компонентов
        move = GetComponent<movePlayerScript>();
        soundControl = GetComponent<SoundControl>();
        soundControl.PlaySound("maintheme"); // Запуск главной темы игровой сцены
        hitTime = defaultHitTime; // Установка стандартного таймера после получения урона
        player.BestCount = savePoints.GetPoint(); // Вызов метода считывания лучшего счета игрока
        player.ShiftSpeedEvent += Player_ShiftSpeedEvent; // Подписка на событые изменения темпа игры
    }

    /// <summary>
    /// Обработчик события изменения темпа игры
    /// </summary>
    /// <param name="newSpeed"></param>
    private void Player_ShiftSpeedEvent(float newSpeed)
    {
        move.moveForce = newSpeed;
    }

    void Update()
    {
        if (isHit)
        {
            hitTime -= Time.deltaTime;

            if (hitTime <= 0)
            {
                // Возврат темпа игры по окончании таймера после получения урона
                move.moveForce = tempMoveForce;
                hitTime = defaultHitTime;
                isHit = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.ToLower() == "obstacle")
        {
            if (!isHit)
            {
                // Запуск звука получения урона
                soundControl.PlaySound("oofsound");

                // Получение темпа игры после получения урона
                tempMoveForce = tempMoveForce != move.moveForce ? move.moveForce : tempMoveForce;

                if (player.Health != 0)
                    player.Health -= 1;

                PushObstacles(collision);

                // Установка минимального темпа игры при получении урона
                move.moveForce = 5;
                // "Поддталкивание" персонажа для иммитации получения урона
                move.JumpCharacter(transform.up * 0.2f);
                isHit = true;
            }
        }

        StopGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "coin")
        {
            soundControl.PlaySound("coinsound");

            player.Points += 1;
        }
    }

    /// <summary>
    /// Метод сохранения счета, остановки движения и вызова меню поражения 
    /// </summary>
    private void StopGame()
    {
        if (player.Health == 0)
        {
            savePoints.SavePoint(player);
            movePlayerScript.isRun = false;
            ActivateLoseMenu();
        }
    }

    /// <summary>
    ///  Метод вызова меню поражения
    /// </summary>
    private void ActivateLoseMenu()
    {
        // Остановка главной темы игровой сцены
        soundControl.StopSound("maintheme");

        // Воспроизведение темы поражения
        if (!soundControl.IsPlaySound("losesound"))
            soundControl.PlaySound("losesound");

        // Активация меню поражения
        loseMenu.gameObject.SetActive(true);
        loseMenu.ShowLoseMenu(player);
    }

    /// <summary>
    ///  Метод отбрасывания препятствия при столкновении
    /// </summary>
    /// <param name="collision"></param>
    private void PushObstacles(Collision collision)
    {
        // Создание вектора направления отбрасывания препятствия
        Vector3 push = (transform.forward + transform.up) * move.moveForce + Vector3.right * Random.Range(-move.moveForce, move.moveForce);
        // Отключение кинематики твердого тела препятствия
        collision.rigidbody.isKinematic = false;
        // Применения силы к препятствию
        collision.rigidbody.AddForce(push, ForceMode.Impulse);
    }

}

