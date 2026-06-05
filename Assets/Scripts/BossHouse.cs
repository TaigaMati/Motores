using UnityEngine;

public class BossHouseTrigger : MonoBehaviour
{
    [Header("Referencia al texto UI")]
    public GameObject stop;

    private bool alreadyShown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadyShown)
        {
            alreadyShown = true;
            stop.gameObject.SetActive(true);

            // APAGAMOS EL TRIGGER: Ya no detectará más choques en toda la partida
            Collider miTrigger = GetComponent<Collider>();
            if (miTrigger != null)
            {
                miTrigger.enabled = false;
            }

            StartCoroutine(HideTextAfterSeconds(3f));
        }
    }

    private System.Collections.IEnumerator HideTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        stop.gameObject.SetActive(false);
    }
}