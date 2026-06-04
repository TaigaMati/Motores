using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    [Header("Parámetros de vida")]
    public int maxHealth = 3;
   private int currentHealth;


    public EnemyController controller;
    public MusicManager musicManager;

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
            if (musicManager != null)
            {
                musicManager.OnEnemyDeath();
            }

            PowerManager.Instance.CutPower();
            controller.Die();
            Destroy(gameObject);
        }
    }

    // 👀 Detectar al jugador
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🎵 Cambio a música de combate");
            if (musicManager != null)
            {
                musicManager.OnEnemyEncounter();
            }
        }
    }
}
