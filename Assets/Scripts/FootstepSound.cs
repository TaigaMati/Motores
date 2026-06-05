using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips;

    // Método llamado desde Animation Event
    public void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            int index = Random.Range(0, footstepClips.Length);
            audioSource.PlayOneShot(footstepClips[index]);
        }
    }
}