using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundSource;
    public AudioSource combatSource;

    public AudioSource viento;

    public AudioClip backgroundClip;
    public AudioClip combatClip;

    public AudioClip vientoclip;

    void Start()
    {
        PlayBackground();
    }

    public void OnEnemyEncounter()
    {
        backgroundSource.Stop();
        combatSource.clip = combatClip;
        combatSource.loop = true;
        combatSource.Play();
    }

    public void OnEnemyDeath()
    {
        combatSource.Stop();
        PlayBackground();
    }

    void PlayBackground()
    {
        backgroundSource.clip = backgroundClip;
        backgroundSource.loop = true;
        backgroundSource.Play();
    }
}
