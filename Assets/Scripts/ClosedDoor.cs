using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    [Header("Referencia de la puerta")]
    public GameObject door;

    [Header("Configuración de sonido")]
    [SerializeField] private AudioClip sonidoCerrar; 
    private AudioSource audioSource;

    private bool activada = false; // estado: se activó una vez

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Se activa una sola vez al entrar
    private void OnTriggerEnter(Collider other)
    {
        if (!activada && other.CompareTag("Player"))
        {
            activada = true; // ya se activó, no vuelve a sonar
            door.SetActive(true);

            if (sonidoCerrar != null)
            {
                audioSource.PlayOneShot(sonidoCerrar);
            }
        }
    }

    // Método público para abrir la puerta cuando muere el enemigo
    public void AbrirPuerta()
    {
        door.SetActive(false); // la puerta se abre
        Debug.Log("🚪 Puerta abierta tras muerte del enemigo");
    }
}
