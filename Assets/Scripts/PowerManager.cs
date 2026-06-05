using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering;
using FMODUnity;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance;
    public static event System.Action OnPowerRestored; // 🔔 evento global

    public StudioEventEmitter LightsOn;
    public StudioEventEmitter LightsOff;

    public bool powerOn = false;

    // para activar panel de texto
    [Header("Referencia al texto UI")]
    public GameObject panelTextCutPower;
    public GameObject panelTextRestorePower;


    void Awake()
    {
        Instance = this;
    }

    public void RestorePower()
    {
        LightsOn.Play();
        powerOn = true;
        Debug.Log("⚡ Energía restaurada");

        OnPowerRestored?.Invoke();

        panelTextRestorePower.gameObject.SetActive(true);
        panelTextCutPower.gameObject.SetActive(false);
        StartCoroutine(HideTextAfterSeconds(5f));
    }

    public void CutPower()
    {
        LightsOff.Play();
        powerOn = false;
        Debug.Log("⚡ Energía cortada");

        panelTextCutPower.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterSeconds(25f));
    }

    // Coroutine duración de los paneles
    private System.Collections.IEnumerator HideTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        panelTextCutPower.gameObject.SetActive(false);
        panelTextRestorePower.gameObject.SetActive(false);
    }
}
