using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    public GameObject door; 

    
    void Start()
    {

    }


    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            door.SetActive(true); 
        }
    }
}
