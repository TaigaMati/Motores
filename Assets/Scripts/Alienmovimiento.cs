using UnityEngine;

public class AlienMovimiento : MonoBehaviour
{
    public Transform destino; 
    public float velocidad = 2f;

    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activado)
        {
            activado = true;
        }
    }

    private void Update()
    {
        if (activado)
        {
            // Mover alien hacia el destino
            transform.position = Vector3.MoveTowards(
                transform.position,
                destino.position,
                velocidad * Time.deltaTime
            );

            // Cuando llega al destino
            if (Vector3.Distance(transform.position, destino.position) < 0.1f)
            {
                gameObject.SetActive(false); // Desaparece
            }
        }
    }
}
