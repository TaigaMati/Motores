using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem; // Importante para el nuevo Input System

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public AudioMixerSnapshot pausedSnapshot;
    public AudioMixerSnapshot unpausedSnapshot;

    private bool isPaused = false;

    // Acción de entrada (se asigna desde el InputActionAsset en el inspector)
    public InputAction pauseAction;

    private void OnEnable()
    {
        pauseAction.Enable();
        pauseAction.performed += OnPausePressed;
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPausePressed;
        pauseAction.Disable();
    }

    private void OnPausePressed(InputAction.CallbackContext context)
    {
        if (isPaused) Resume();
        else Pause();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pausedSnapshot.TransitionTo(0f); // transición inmediata
        isPaused = true;

        // Mostrar cursor para poder usar los botones
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        unpausedSnapshot.TransitionTo(0f); // transición inmediata
        isPaused = false;

        // Ocultar cursor y bloquearlo para volver al control normal del juego
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        // Al salir también ocultamos el cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Application.Quit();
    }
}
