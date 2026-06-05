using UnityEngine;

public class TutorialZone : MonoBehaviour
{
    [Header("Audio del tutorial")]
    public AudioSource tutorialAudio;

    private bool reproducido = false; // para que suene solo una vez

    private void OnTriggerEnter(Collider other)
    {
        if (!reproducido && other.CompareTag("Player"))
        {
            reproducido = true;
            if (tutorialAudio != null)
            {
                tutorialAudio.Play();
                Debug.Log("📢 Tutorial activado en la zona");
            }
        }
    }
}
