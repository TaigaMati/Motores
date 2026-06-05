using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private EnemyGiant giant;
    [SerializeField] private string victorySceneName = "VictoryScene"; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            if (giant != null && giant.isDead)
            {
                Debug.Log(" Has escapado");
                SceneManager.LoadScene(victorySceneName);
            }
            else
            {
                Debug.Log(" El gigante sigue vivo, no se puede ganar todavía.");
            }
        }
    }
}
