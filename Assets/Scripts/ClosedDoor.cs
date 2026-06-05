using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    public GameObject door;

    // El candado lógico: falso significa que la puerta todavía no se cerró
    private bool yaSeCerro = false;

    private void OnEnable()
    {
        Enemy.OnEnemyDefeated += AbrirPuerta;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDefeated -= AbrirPuerta;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. Verificamos que sea el Player
        // 2. ¡Y verificamos que el candado esté abierto (!yaSeCerro)!
        if (other.CompareTag("Player") && !yaSeCerro)
        {
            // Cerramos el candado inmediatamente. Ya nadie más puede entrar a este IF
            yaSeCerro = true;

            if (door != null)
            {
                door.SetActive(true);
                Debug.Log("?? Player detectado. Puerta cerrada. Candado activado.");
            }

            // Desactivamos el colisionador físico por seguridad
            Collider miTrigger = GetComponent<Collider>();
            if (miTrigger != null)
            {
                miTrigger.enabled = false;
            }
        }
    }

    private void AbrirPuerta()
    {
        Debug.Log("?? El Trigger recibió el evento de muerte del enemigo.");

        if (door != null)
        {
            door.SetActive(false);
            Debug.Log("?? Puerta abierta con éxito.");
        }
    }
}