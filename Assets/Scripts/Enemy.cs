using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Parámetros de vida")]
    public int maxHealth = 3;

    private int currentHealth;
    public StudioEventEmitter Hurt;
    public StudioEventEmitter Ded;

    public EnemyController controller;

    // Este es el evento global que avisará a otros scripts que este enemigo murió
    public static event Action OnEnemyDefeated;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Hurt.Play();
        currentHealth -= amount;

        Debug.Log("⚔️ " + gameObject.name +
                  " recibió " + amount +
                  " de daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Ded.Play();
            PowerManager.Instance.CutPower();

            // 🔥 ¡ESTA LÍNEA FALTABA! Avisa a la puerta antes de morir.
            if (OnEnemyDefeated != null)
            {
                OnEnemyDefeated.Invoke();
            }

            controller.Die();
        }
    }
}