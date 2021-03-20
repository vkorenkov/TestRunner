using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayerScript : MonoBehaviour
{
    
    /// <summary>
    ///  Компонент твердого тела игрока
    /// </summary>
    Rigidbody rb;
    /// <summary>
    /// Сила прыжка игрока
    /// </summary>
    Vector3 jumpForce;
    /// <summary>
    /// Текущая позиция игрока на дороге
    /// </summary>
    int playerCurrentPosition = 1;
    /// <summary>
    /// Количество "линий" на дороге
    /// </summary>
    int roadPositionsCount = 2;
    /// <summary>
    /// Нулевая "линия" на дороге (левая)
    /// </summary>
    [SerializeField] float leftPosition;
    /// <summary>
    /// Дистанция между "линиями" на дороге
    /// </summary>
    [SerializeField] float roadPositionsDistance;
    /// <summary>
    /// Скорость передвижения между "линиями"
    /// </summary>
    [SerializeField, Range(1, 10)] float slideSpeed;
    /// <summary>
    /// Переменная указывает в какую сторону была нажата кнопка
    /// </summary>
    float playerInput;
    /// <summary>
    /// Переменная указывает, была ли нажата кнопка.
    /// </summary>
    bool isPressed = false;
    /// <summary>
    /// Параметр высоты прыжка игрока
    /// </summary>
    [SerializeField, Range(2, 10)] private float playerJump = 2f;
    /// <summary>
    /// Сила воздействия на модель игрока для движения вперед
    /// </summary>
    [SerializeField, Range(0, 50)] public float moveForce = 20;
    public static bool isRun;

    void Start()
    {
        // Получение твердого тела игрока
        rb = GetComponent<Rigidbody>();
        isRun = true;
    }

    void Update()
    {
        // Условие нажатия кнопки прыжка
        if (Input.GetButtonDown("Jump"))
        {
            jumpForce = Vector3.up;
        }

        // Получение значения перемещения игрока
        playerInput = Input.GetAxis("Horizontal");

        // Вызов метода перемещения игрока
        SlideMove();
    }

    private void FixedUpdate()
    {
        if (isRun)
        {
            // Движение персонажа, если дорога неподвижна
            //transform.Translate(transform.forward * moveForce * Time.deltaTime);

            // Передвижение дороги для создания эффекта движения
            foreach (var r in RoadSpawnScript.currentRoad)
            {
                r.transform.Translate(r.transform.forward * moveForce * Time.deltaTime);
            }
        }

        #region физическое движение персонажа
        //// Условие сохранения скорости игрока (не смог решить проблему "спотыкания" персонажа о стыки коллайлеров)
        //if (rb.velocity.magnitude < moveForce)
        //{
        //    // Применение силы для движения игрока
        //    rb.AddForce(transform.forward * moveForce, ForceMode.Force);
        //}
        #endregion

        // Вызов метода прыжка игрока
        JumpCharacter(jumpForce);

        // Сброс вектора прыжка
        jumpForce = new Vector3();
    }

    /// <summary>
    /// Метод перемещения игрока по "линиям" дороги
    /// </summary>
    void SlideMove()
    {
        // Получение абсолютного числа (нажатие кнопки)
        if (Mathf.Abs(playerInput) > 0.01f)
        {
            // Условие указывает на то нажата кнопка или нет
            if (!isPressed)
            {
                isPressed = true;
                // Получение знака при вводе игрока
                playerCurrentPosition += (int)Mathf.Sign(playerInput);
                // Ограничение выхода за пределы игровой зоны
                playerCurrentPosition = Mathf.Clamp(playerCurrentPosition, 0, roadPositionsCount);
            }
        }
        else
            isPressed = false;

        // Получение текущей позиции игрока
        Vector3 newPosition = transform.position;
        // "Сглаживание" перемещения игрока от одной "линии" к другой
        newPosition.x = Mathf.Lerp(newPosition.x, leftPosition + (playerCurrentPosition * roadPositionsDistance), slideSpeed * Time.deltaTime);
        // Установка позиции игрока
        transform.position = newPosition;
    }

    /// <summary>
    /// Метод прыжка игрока
    /// </summary>
    /// <param name="jump"></param>
    public void JumpCharacter(Vector3 jump)
    {
        // создние луча вниз от игрока
        Ray ray = new Ray(transform.position, -transform.up);
        // Получение пересечения луча и поверхрости
        RaycastHit hit;
        // Условие позволяющее сделать прыжок
        if (Physics.Raycast(ray, out hit, 1))
        {
            // Применение усилия для "подбрасывания" игрока
            rb.AddForce(jump * playerJump, ForceMode.Impulse);
        }
    }
}
