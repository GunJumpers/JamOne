using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : UnitySingleton<GameManager>
{

    [Serializable]
    public struct RoomCompletionEvent{

        public RoomType roomType;
        public AK.Wwise.Event soundEvent;
        public GameObject source;

    }

    public Dictionary<BasePuzzleRoom, bool> puzzleRooms;
    public enum RoomType {Cubes, Lasers, Aquarium, Racoon, Maze, Simon }
    [SerializeField] public List<RoomCompletionEvent> puzzleCompletionEvents;
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

    public void RestartScene()
    {
        Time.timeScale = 1;
        PlayerController.Instance.canControlMovement = true;
        FPPUIController.Instance.TogglePauseMenu(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(0);
    }

    public void CompletePuzzleRoom(RoomType type)
    {
       foreach(RoomCompletionEvent roomEvent in puzzleCompletionEvents)
        {
            if(roomEvent.roomType == type)
            {
                Debug.Log(type.ToString() + " was COMPLETED!!!");
                roomEvent.soundEvent.Post(roomEvent.source);
            }
        }
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
