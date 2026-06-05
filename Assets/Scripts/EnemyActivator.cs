using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    [Header("Prefab del enemigo")]
    public GameObject enemyPrefab; 

    private void OnEnable()
    {
        PowerManager.OnPowerRestored += ActivarEnemigo;
    }

    private void OnDisable()
    {
        PowerManager.OnPowerRestored -= ActivarEnemigo;
    }

    private void ActivarEnemigo()
    {
        if (enemyPrefab != null)
        {
            enemyPrefab.SetActive(true);
            Debug.Log("👹 Enemigo activado al restaurar energía");
        }
    }
}
