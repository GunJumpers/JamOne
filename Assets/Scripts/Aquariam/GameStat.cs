using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStat : UnitySingleton<GameStat>
{
    public GameObject Fishes;
    public List<FishMovement> fishControllers;
    public Text Warning;
    public Text Announcement;
    public static bool GameOver = false;
    public static bool playerIsEntered = false;
    public static bool radioUsed = false;
    public static bool isCompleted;
    public static bool isActive;
    [SerializeField] private GameObject winningGoal;
    [SerializeField] private GameObject radioPrefab;
    [SerializeField] private Transform spawnPosition;
    public AK.Wwise.Event radioStartSFX = null;
    public AK.Wwise.Event aquariumWinSound = null;
    private Vector3 startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        DisablePuzzle();
        for(int i = 0; i < Fishes.transform.childCount; i++)
        {
            fishControllers.Add(Fishes.transform.GetChild(i).GetComponent<FishMovement>());
        }
        startPosition = spawnPosition.transform.position;
    }

    void Init()
    {
        Warning.gameObject.SetActive(false);
        Announcement.gameObject.SetActive(true);
        Announcement.text = "RIGHT CLICK TO USE RADIO";
        radioPrefab.SetActive(false);
        winningGoal.SetActive(false);
    }

    public void EnablePuzzle()
    {
        isActive = true;
        Init();
    }

    public void DisablePuzzle()
    {
        isActive = false;
        DisableAllElements();
    }

    void DisableAllElements()
    {
        radioPrefab.SetActive(false);
        radioUsed = false;
        Announcement.gameObject.SetActive(false);
        winningGoal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Announcement.gameObject.SetActive(false);
            radioStartSFX.Post(gameObject);
            radioPrefab.SetActive(!radioPrefab.activeInHierarchy);
            radioUsed = !radioUsed;

        }

        if (GameOver)
        {
            GameOverScreen();
        }

    }

    private void GameOverScreen()
    {
        changeText(Warning, "GAME OVER", true);
        changeText(Announcement, "PRESS SPACE TO RESTART", true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameOver = false;
            playerIsEntered = false;
            Warning.gameObject.SetActive(false);
            PlayerController.Instance.transform.position = startPosition;
            for (int i = 0; i < Fishes.transform.childCount; i++)
            {
                GameObject Fish = Fishes.transform.GetChild(i).gameObject;
                Fish.GetComponent<FishMovement>().FishReset();
            }
            winningGoal.SetActive(false);
        }
    }

    public void FinishLevel()
    {
        Debug.Log("aquarium level done");
        aquariumWinSound.Post(this.gameObject);
    }

    private void WinnerScreen()
    {

            /*
            changeText(Announcement, "Fish ♡ Radio", true);
            if (Input.anyKeyDown)
            {
                Announcement.gameObject.SetActive(false);
            }
            */

            winningGoal.SetActive(true);
            changeText(Announcement, "Fish ♡ Radio", true);
        
    }

    public void CheckPuzzleComplete()
    {
        foreach(FishMovement f in fishControllers)
        {
            if (!f.isSoothed)
            {
                return;
            }
        }


        WinnerScreen();
    }

    void changeText(Text text,string textValue, bool state)
    {
        text.text = textValue;
        text.gameObject.SetActive(state);
    }
}
