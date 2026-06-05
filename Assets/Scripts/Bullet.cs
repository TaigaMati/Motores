using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Daño de la bala")]
    public int damage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("💥 Bala impactó contra: " + collision.gameObject.name);

        // Caso 1: enemigo normal con script Enemy
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"⚔️ Se aplicó {damage} de daño a Enemy");
        }

        // Caso 2: enemigo gigante con script EnemyGiant
        EnemyGiant giant = collision.gameObject.GetComponent<EnemyGiant>();
        if (giant != null)
        {
            giant.RecibirDanio(damage);
            Debug.Log($"⚔️ Se aplicó {damage} de daño a EnemyGiant");
        }

        // La bala se destruye al impactar
        Destroy(gameObject);
    }
}
