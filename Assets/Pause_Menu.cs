using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu_Panel : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        }
    }
}
