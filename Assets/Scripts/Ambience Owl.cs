using UnityEngine;
using FMODUnity;

public class AmbienceOwl : MonoBehaviour
{
    public StudioEventEmitter Buho;

    private void Start()
    {
        Buho.Play();
    }
}