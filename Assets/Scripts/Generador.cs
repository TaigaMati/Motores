using UnityEngine;

public class Generador : MonoBehaviour
{public bool hasFuel = false;

    public void AddFuel(GameObject fuelObject)
    {
        hasFuel = true;

        Destroy(fuelObject);

        Debug.Log("🛢️ Nafta colocada");
    }

    public void TurnOn()
    {
        if (hasFuel)
        {
            PowerManager.Instance.RestorePower();

            Debug.Log("⚡ Generador encendido");
        }
        else
        {
            Debug.Log("❌ Necesitás nafta");
        }
    }
}
