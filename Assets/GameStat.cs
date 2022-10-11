using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStat : MonoBehaviour
{
    public static bool GameOver = false;
    public GameObject Goal;
    public GameObject Fishes;
    public GameObject Radio;
    public Text Warning;
    public Text Announcement;
    public static bool playerIsEntered = false;
    public static bool isSoothed = false;
    public static bool radioUsed = false;
    public static bool isCompleted;
    private GameObject radioPrefab;
    private Vector3 startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        Warning.gameObject.SetActive(false);
        Announcement.text = "PRESS ANY KEY TO START";
        radioPrefab = gameObject.transform.GetChild(0).GetChild(2).gameObject;
        radioPrefab.SetActive(false);
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Announcement.text = "LEFT CLICK TO USE RADIO";

            if (Input.GetMouseButtonDown(1))
            {
                Announcement.gameObject.SetActive(false);
                radioPrefab.SetActive(!radioPrefab.activeInHierarchy);
                radioUsed = !radioUsed;
            }
        }
        if (GameOver)
        {
            GameOverScreen();
        }
        else
        {
            WinnerScreen();
        }
    }

    private void GameOverScreen()
    {
        Warning.text = "GAME OVER";
        Warning.gameObject.SetActive(true);
        Announcement.text = "PRESS SPACE TO RESTART";
        Announcement.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameOver = false;
            playerIsEntered = false;
            Warning.gameObject.SetActive(false);
            gameObject.transform.position = startPosition;
            for (int i = 0; i < Fishes.transform.childCount; i++)
            {
                GameObject Fish = Fishes.transform.GetChild(i).gameObject;
                Fish.GetComponent<FishMovement>().FishReset();
            }
        }
    }

    private void WinnerScreen()
    {
        if (isCompleted)
        {
            Announcement.text = "Fish ♡ Radio";
            Announcement.gameObject.SetActive(true);
        }
    }
}
