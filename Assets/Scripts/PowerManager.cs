using UnityEngine;
using UnityEngine.Rendering;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance;

    public bool powerOn = false;

    // para activar panel de texto
    [Header("Referencia al texto UI")]
    public GameObject panelText;
    private bool activePanelText = false;

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
        // para activar panel de texto
        activePanelText = true;
        panelText.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterSeconds(15f));
    }

    private System.Collections.IEnumerator HideTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        panelText.gameObject.SetActive(false);
    }
}
