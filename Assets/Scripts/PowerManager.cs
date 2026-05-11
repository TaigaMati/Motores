using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance;

    public bool powerOn = true;

    private void Awake()
    {
        Instance = this;
    }

    public void CutPower()
    {
        powerOn = false;
    }

    public void RestorePower()
    {
        powerOn = true;
    }
}