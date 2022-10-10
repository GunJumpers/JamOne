using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStat : MonoBehaviour
{
    public static bool GameOver = false;
    public GameObject Goal;
    public GameObject Fish;
    public Text Warning;
    public Text Announcement;
    public static bool playerIsEntered = false;
    public static bool isSoothed = false;
    // Start is called before the first frame update
    void Start()
    {
        Warning.gameObject.SetActive(false);
        Announcement.text = "PRESS ANY KEY TO START";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Announcement.gameObject.SetActive(false);
        }
        if (GameOver)
        {
            GameOverScreen();
        }
    }

    void GameOverScreen()
    {
        Warning.text = "GAME OVER";
        Warning.gameObject.SetActive(true);
        Announcement.text = "PRESS SPACE TO RESTART";
        Warning.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameOver = false;
            playerIsEntered = false;
            Warning.gameObject.SetActive(false);
        }
    }
}
