using UnityEngine;

public class Apagon : MonoBehaviour
{
    private Light[] lights;

    void Start()
    {
           lights = GetComponentsInChildren<Light>();
    }

    void Update()
    {
        if (PowerManager.Instance != null)
        {
            foreach (Light lightComp in lights)
            {
                if (lightComp != null)
                {
                    lightComp.enabled = PowerManager.Instance.powerOn;
                }
            }
        }
    }
}
