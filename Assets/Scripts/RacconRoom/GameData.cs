using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
public class GameData : MonoBehaviour
{
    public NoteSpawner Notes;
    public Bullet HitNotes;
    public static float totalNotes;
    public static float collectNotes;
    public static bool GameEnd;
    public static bool isCompleted;
    public static bool startPlaying;
    public float targetTime;
    public float notesPercentage;
    public Text Announcement, Win, GameTime, Lose, Score;


    private void Start()
    {
        Init();
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Announcement.gameObject.SetActive(false);
            startPlaying = true;

        }
        if (startPlaying)
        {
            targetTime -= Time.deltaTime;
            GameTime.text = "Time: " + Mathf.Floor(((int)targetTime) / 60).ToString("00") + ":" + Mathf.FloorToInt(((int)targetTime) % 60).ToString("00");
            if (targetTime <= 0.0f)
            {
                startPlaying = false;
                GameEnd = true;
                GameResults();
            }
        }
    }

    void GameResults()
    {
        if (GameEnd)
        {
            notesPercentage = collectNotes / totalNotes;
            if (notesPercentage > 0.8)
            {
                Win.text = "YOU WIN";
                Score.text = "Score: " + ((int)(notesPercentage * 100)).ToString() + "%";
                Win.gameObject.SetActive(true);
                isCompleted = true;
            }
            else
            {
                Lose.text = "YOU LOSE";
                Score.text = "Score: " + ((int)(notesPercentage * 100)).ToString() + "%";
                Lose.gameObject.SetActive(true);
                Announcement.text = "Press TO RESTART";
            }
            if (Input.anyKeyDown)
            {
                Init();
            }
        }
        
    }

    private void Init()
    {
        targetTime = 40f;
        Announcement.text = "Press SPACE to Start";
        Announcement.gameObject.SetActive(true);
        isCompleted = false;
        GameEnd = false;
        startPlaying = false;
        collectNotes = 0;
        totalNotes = 0;
        Lose.gameObject.SetActive(true);
        Win.gameObject.SetActive(true);
        Score.gameObject.SetActive(true);
        if (isCompleted)
        {
            Notes.enabled = false;
            HitNotes.enabled = false;
        }
    }
}
