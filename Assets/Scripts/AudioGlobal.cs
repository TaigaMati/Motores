using UnityEngine;
using FMODUnity;

public class GlobalAudioManager : MonoBehaviour
{
   
    public StudioEventEmitter ambient;

    
    public StudioEventEmitter music;

    private void Start()
    {
        // Reproducir ambiente
        if (ambient != null)
            ambient.Play();

        // Reproducir música
        if (music != null)
            music.Play();
    }
}