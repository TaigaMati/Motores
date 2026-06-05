using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundSource;
    public AudioSource combatSource;

    public AudioSource viento;

    [Header("Audio Clips")]
    public AudioClip backgroundClip;
    public AudioClip combatClip;

    public AudioClip vientoclip;

    [Header("Audio Mixer")]
    public AudioMixer mixer;
    public string exposedParam = "MusicVolume"; // parámetro expuesto en el Mixer

    void Start()
    {
        PlayBackground();
        PlayViento();
    }

    public void OnEnemyEncounter()
    {
        // 🔄 Cambiar a música de combate
        backgroundSource.Stop();
        combatSource.clip = combatClip;
        combatSource.loop = true;
        combatSource.Play();

        // Modificar parámetro del Mixer en tiempo real
        mixer.SetFloat(exposedParam, 0f); // volumen normal
    }

    public void OnEnemyDeath()
    {
        // 🔄 Volver a música de exploración
        combatSource.Stop();
        PlayBackground();

        // Atenuar volumen para transición
        mixer.SetFloat(exposedParam, -10f); // baja el volumen
    }

    void PlayBackground()
    {
        backgroundSource.clip = backgroundClip;
        backgroundSource.loop = true;
        backgroundSource.Play();
    }

    void PlayViento()
    {
        viento.clip = vientoclip;
        viento.loop = true;
        viento.Play();
    }
}
