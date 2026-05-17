using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDaño : MonoBehaviour
{
    [Header("Configuración de Vida")]
    [SerializeField] private int vidaMaxima = 100;

    // Propiedad pública de solo lectura para otros scripts
    public int VidaActual { get; private set; }

    [Header("Configuración de Escenas")]
    [SerializeField] private string EscenaDerrota = "Derrota";

    private void Awake()
    {
        // Inicializamos la vida al empezar el juego
        VidaActual = vidaMaxima;
    }

   
    public void RecibirDanio(int cantidad)
    {
        // Validamos que no nos pasen daño negativo
        if (cantidad <= 0) return;

        VidaActual -= cantidad;
        Debug.Log($"El jugador recibió daño. Vida restante: {VidaActual}");

        // Controlamos que la vida no baje de 0
        if (VidaActual <= 0)
        {
            VidaActual = 0;
            ProcesarMuerte();
        }
    }

    private void ProcesarMuerte()
    {
        Debug.Log("Jugador muerto. Cargando escena de derrota...");

        // Aseguramos que el tiempo del juego corra normal antes de cambiar de escena
        Time.timeScale = 1f;

        // Cambiamos a la escena de derrota
        SceneManager.LoadScene(EscenaDerrota);
    }
}