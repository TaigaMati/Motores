using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDaño : MonoBehaviour
{
    [Header("Configuración de Vida")]
    [SerializeField] private int vidaMaxima = 100;
    
    public int VidaActual { get; private set; }

    [Header("Configuración de Escenas")]
    [SerializeField] private string EscenaDerrota = "Derrota";

    [Header("Configuración de Sonido")]
    [SerializeField] private AudioClip sonidoDaño;   // Clip de sonido al recibir daño
    private AudioSource audioSource;                 // Reproductor de audio

    private void Awake()
    {
         
        VidaActual = vidaMaxima;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void RecibirDanio(int cantidad)
    {
        
        if (cantidad <= 0) return;

        VidaActual -= cantidad;
        Debug.Log($"El jugador recibió daño. Vida restante: {VidaActual}");

        // 🔊 Reproducir sonido de daño
        if (sonidoDaño != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }

        if (VidaActual <= 0)
        {
            VidaActual = 0;
            ProcesarMuerte();
        }
    }

    private void ProcesarMuerte()
    {
        Debug.Log("Jugador muerto. Cargando escena de derrota...");
        
        Time.timeScale = 1f;
         
        SceneManager.LoadScene(EscenaDerrota);
    }
}
