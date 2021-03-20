using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsScript : MonoBehaviour
{
    Animator animator;

    PlayerParameters player;

    Rigidbody rb;

    private void Start()
    {
        // Получение компонентов с объекта игрока
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerParameters>();
        // Запуск анимации бега при старте игровой сцены
        animator.SetBool("run", movePlayerScript.isRun);
    }

    void Update()
    {
        // Запуск анимации прыжка
        animator.SetFloat("jump", Mathf.Abs(rb.velocity.y));

        if(player.player.Health == 0)
        {
            // переход в анимацию бездействия
            animator.SetBool("run", movePlayerScript.isRun);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ground")
        {
            // Запуск анимации приземления
             animator.SetTrigger("grounded");
        }
    }
}
