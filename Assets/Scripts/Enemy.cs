using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    [Header("Parámetros de vida")]
    public int maxHealth = 3;

    private int currentHealth;

    // Referencia al controlador del enemigo
    public EnemyController controller;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        Debug.Log("⚔️ " + gameObject.name +
                  " recibió " + amount +
                  " de daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            // APAGA TODAS LAS LUCES
            PowerManager.Instance.CutPower();

            // MUERTE DEL ENEMIGO
            controller.Die();
        }
    }
}