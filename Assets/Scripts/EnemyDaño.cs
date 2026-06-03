using FMODUnity;
using UnityEngine;

public class EnemyDaño : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    [SerializeField] private int danioAtaque = 20;
    [SerializeField] private float rangoAtaque = 2.0f;
    public StudioEventEmitter Attack;
    // El Tag que buscaremos en el jugador (por defecto "Player")
    [SerializeField] private string tagJugador = "Player";

    [SerializeField] private Transform puntoAtaque;
        

    private void Awake()
    {
       
        if (puntoAtaque == null)
        {
            puntoAtaque = transform;
        }
    }

    /// <summary>
    /// ESTE MÉTODO LO LLAMARÁ LA ANIMACIÓN.
    /// Filtra a los objetos cercanos buscando el Tag correcto.
    /// </summary>
    public void EventoGolpeEnemigo()
    {
        // Detecta TODOS los colisionadores en el rango de ataque
        Collider[] objetosImpactados = Physics.OverlapSphere(puntoAtaque.position, rangoAtaque);
        Attack.Play();

        foreach (Collider hit in objetosImpactados)
        {
            // Verificamos si el objeto tiene el Tag del jugador
            if (hit.CompareTag(tagJugador))
            {
                // Buscamos el script de vida que creamos antes
                PlayerDaño vidaJugador = hit.GetComponent<PlayerDaño>();

                if (vidaJugador != null)
                {
                    vidaJugador.RecibirDanio(danioAtaque);
                    Debug.Log($"¡Enemigo golpeó al Player usando Tag! Daño: {danioAtaque}");

                    // Rompemos el bucle para no golpear múltiples veces en el mismo frame
                    break;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null) return;
        Attack.Play();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }
}