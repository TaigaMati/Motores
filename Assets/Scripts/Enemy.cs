using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    [Header("Parámetros de vida")]
    public int maxHealth = 3;

    private int currentHealth;
    public StudioEventEmitter Hurt;
    public StudioEventEmitter Ded;

    public EnemyController controller;

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

            
            controller.Die();
        }
    }
}