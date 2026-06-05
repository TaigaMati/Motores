using UnityEngine;

public class CombatZone : MonoBehaviour
{
    public MusicManager musicManager;
    public ClosedDoor puerta; // referencia a la puerta

    private bool activa = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!activa && other.CompareTag("Player"))
        {
            activa = true;
            Debug.Log("🎵 Zona de combate activada");

            if (musicManager != null)
                musicManager.OnEnemyEncounter();

            // cerrar la puerta al entrar
            if (puerta != null)
                puerta.door.SetActive(true);
        }
    }

    public void DesactivarZona()
    {
        activa = false;
        Debug.Log("🎵 Zona de combate desactivada");

        if (musicManager != null)
            musicManager.OnEnemyDeath();

        // abrir la puerta al desactivar la zona
        if (puerta != null)
            puerta.AbrirPuerta();
    }
}
