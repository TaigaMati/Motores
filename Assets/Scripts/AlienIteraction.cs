using UnityEngine;

public class AlienAbduction : MonoBehaviour
{
    public GameObject alien;
    public GameObject nave;
    public Light luzVerde;
    public Transform destinoAlien; 
    public Vector3 movimientoNave = new Vector3(0, 0, 10);

    // 🚀 Velocidades altas para movimiento rápido
    public float velocidadAlien = 25f;
    public float velocidadNave = 30f;

    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activado)
        {
            activado = true;
            StartCoroutine(Secuencia());
        }
    }

    private System.Collections.IEnumerator Secuencia()
    {
        // Encender luz al iniciar interacción
        luzVerde.enabled = true;

        // Alien sube rápido
        while (Vector3.Distance(alien.transform.position, destinoAlien.position) > 0.1f)
        {
            alien.transform.position = Vector3.MoveTowards(
                alien.transform.position,
                destinoAlien.position,
                velocidadAlien * Time.deltaTime
            );
            yield return null;
        }

        // Apagar luz al llegar
        luzVerde.enabled = false;

        // Mover nave rápido
        Vector3 destinoNave = nave.transform.position + movimientoNave;
        while (Vector3.Distance(nave.transform.position, destinoNave) > 0.1f)
        {
            nave.transform.position = Vector3.MoveTowards(
                nave.transform.position,
                destinoNave,
                velocidadNave * Time.deltaTime
            );
            yield return null;
        }

        // Desactivar alien y nave
        alien.SetActive(false);
        nave.SetActive(false);
    }
}
