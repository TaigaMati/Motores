using FMODUnity;
using UnityEngine;

public class Generador : MonoBehaviour
{
    public bool hasFuel = false;
    public StudioEventEmitter GeneradorOut;
    public StudioEventEmitter GeneradorOn;
    public StudioEventEmitter GeneradorRunning;
    public StudioEventEmitter Pouring;

    public void AddFuel(GameObject fuelObject)
    {
         Pouring.Play();
         hasFuel = true;
         Destroy(fuelObject);
         Debug.Log(" Nafta colocada");
         TurnOn(); 
    }

    public void TurnOn()
    {
        if (hasFuel)
        {
            GeneradorOn.Play();
            PowerManager.Instance.RestorePower();
            Debug.Log(" Generador encendido");
        }
        else
        {
            Debug.Log(" Necesitás nafta");
        }
    }

    public void TurnOff()
    {
        PowerManager.Instance.CutPower();
        Debug.Log(" Generador apagado");
    }
}
