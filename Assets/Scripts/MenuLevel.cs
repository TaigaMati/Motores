using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevel : MonoBehaviour
{
  
    public void ReturnToMainMenu()
    {
        Debug.Log("¡El botón se presionó correctamente! Intentando cargar escena...");
        SceneManager.LoadScene("MenuInicio");
    }

}