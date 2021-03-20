using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    /// <summary>
    /// Компонент аниматор монеты
    /// </summary>
    Animator animator;

    /// <summary>
    /// Компонент частицы монеты
    /// </summary>
    ParticleSystem particle;

    private void Start()
    {
        // Полкчение компонентов
        animator = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "player")
        {
            particle.Play();
            animator.SetTrigger("collect");
        }
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
