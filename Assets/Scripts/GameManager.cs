using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : UnitySingleton<GameManager>
{

    public Dictionary<BasePuzzleRoom, bool> puzzleRooms;
    public bool isPaused;

    public override void Awake()
    {
        base.Awake();
        puzzleRooms = new Dictionary<BasePuzzleRoom, bool>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnPaused(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            PlayerController.Instance.canControlMovement = true;
            FPPUIController.Instance.TogglePauseMenu(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0;
            PlayerController.Instance.canControlMovement = false;
            FPPUIController.Instance.TogglePauseMenu(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

        }

        isPaused = !isPaused;
    }


}
