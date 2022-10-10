using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public NoteSpawner Notes;
    public Bullet HitNotes;
    public float totalNotes;
    public static float collectNotes;
    public Conductor music;
    public bool startPlaying = false;
    public static bool GameEnd;
    public float targetTime;
    public float notesPercentage;
    public Text Announcement, Win, GameTime, Lose, Score;

    // Start is called before the first frame update
    void Start()
    {
        targetTime = music.musicSource.clip.length;

    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            Announcement.text = "Press Button to Start";
            if (Input.anyKeyDown)
            {
                Announcement.gameObject.SetActive(false);
               startPlaying = true;
                music.musicSource.Play();
                Notes.hasStarted = true;
            }

        }

        else
        {
            targetTime -= Time.deltaTime;
            GameTime.text = "Time: " + Mathf.Floor(((int)targetTime) / 60).ToString("00") + ":" + Mathf.FloorToInt(((int)targetTime) % 60).ToString("00");
            if (targetTime <= 0.0f)
            {
                timerEnded();
            }
        }
    }

    void timerEnded()
    {
        Debug.Log("THE END");
        Notes.hasStarted = false;
        startPlaying = false;
        GameEnd = true;
        GameResults();
    }

    void GameResults()
    {
        if (GameEnd)
        {
            notesPercentage = collectNotes / totalNotes;
            Debug.Log(notesPercentage);
            if (notesPercentage > 0.8)
            {
                Win.text = "YOU WIN";
                Score.text = ((int)(notesPercentage * 100)). ToString() + "%" ;
                Win.gameObject.SetActive(true);
            }
            else
            {
                Lose.text = "YOU LOSE";
                Score.text = ((int)(notesPercentage * 100)).ToString() + "%";
                Lose.gameObject.SetActive(true);
            }
        }
    }

}
