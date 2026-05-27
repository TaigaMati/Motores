using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance;

    public bool powerOn = false;

    void Awake()
    {
        Instance = this;
    }
    public void RestorePower()
    {
        powerOn = true;
        Debug.Log(" Energía restaurada");
    }

    public void CutPower()
    {
        powerOn = false;
        Debug.Log(" Energía cortada");
    }
}
