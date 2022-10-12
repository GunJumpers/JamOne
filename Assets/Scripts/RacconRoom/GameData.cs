using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameData : MonoBehaviour 
{
    public NoteSpawner Notes;
    public Bullet HitNotes;
    public float totalNotes;
    public static float collectNotes;
    public bool startPlaying = false;
    public static bool GameEnd;
    public float targetTime;
    public float notesPercentage;
    public Conductor music;
    public Text Announcement, Win, GameTime, Lose, Score;
    public float beats;

    private void Start()
    {
        
        Announcement.text = "Press SPACE to Start";
        targetTime = 40;
    }
    // Update is called once per frame
    void Update()
    {
        beats = Random.Range(1f, 4f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Announcement.gameObject.SetActive(false);
            Notes.changeNoteTempo(Random.Range(1, 3), beats);
            startPlaying = true;
            Notes.hasStarted = true;

        }

        if (startPlaying)
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
                Score.text = ((int)(notesPercentage * 100)).ToString() + "%";
                Win.gameObject.SetActive(true);
            }
            else
            {
                Lose.text = "YOU LOSE\nPress To Restart";
                Score.text = "Score: " + ((int)(notesPercentage * 100)).ToString() + "%";
                Lose.gameObject.SetActive(true);
            }
        }
    }

}
