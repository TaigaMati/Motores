using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDerrota : MonoBehaviour
{
    // Llama a esto cuando el jugador muera para ir a la escena de derrota
    public void CargarEscenaDerrota()
    {
        // Asegúrate de que el tiempo corra normal por si acaso
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Derrota"); // <--- Pon acá el nombre EXACTO de tu escena de derrota
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MenuInicio"); 
    }

    public void QuitGame()
    {
        Debug.Log("El jugador salió del juego.");
        Application.Quit();
    }
}