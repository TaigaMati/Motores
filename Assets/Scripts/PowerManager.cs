using UnityEngine;
using UnityEngine.Rendering;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance;

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
        powerOn = true;
        Debug.Log(" Energía restaurada");
        // para activar el panel de texto
        panelTextRestorePower.gameObject.SetActive(true);
        panelTextCutPower.gameObject.SetActive(false);
        StartCoroutine(HideTextAfterSeconds(5f));
    }

    public void CutPower()
    {
        powerOn = false;
        Debug.Log(" Energía cortada");
        // para activar panel de texto
        panelTextCutPower.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterSeconds(15f));
    }

    // Coroutine duración de los paneles
    private System.Collections.IEnumerator HideTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        panelTextCutPower.gameObject.SetActive(false);
        panelTextRestorePower.gameObject.SetActive(false);
    }
}
