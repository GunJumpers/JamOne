using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RaccoonGameData : MonoBehaviour
{
    public NoteSpawner Notes;
    public Bullet HitNotes;
    public GameObject playerGun;
    public static Transform NoteSpawnerPoint;
    public static float totalNotes;
    public static float collectNotes;
    public static bool GameEnd;
    public static bool isCompleted;
    public static bool startPlaying;
    public float targetTime;
    public float notesPercentage;
    public Text Announcement, Win, GameTime, Lose, Score;
    public RoomDetector roomDetector;
    public bool isEnabled;
    public bool isWon;

    public Animator raccoonAnim;

    [Header("Win Goal Objects")]
    public GameObject winGoalPrefab;
    public Transform winGoalSpawnPosition;

    public void RestartScenePuzzle()
    {
        GameEnd = false;
        isCompleted = false;
    }

    private void Start()
    {
        RestartScenePuzzle();
        DisablePuzzle();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isEnabled)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isWon)
        {
            Announcement.gameObject.SetActive(false);
            startPlaying = true;
            playerGun.SetActive(true);

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
            playerGun.SetActive(false);
            if (notesPercentage > 0.8)
            {
                WinGame();
            }
            else
            {
                Lose.text = "YOU LOSE";
                Score.text = "Score: " + ((int)(notesPercentage * 100)).ToString() + "%";
                Lose.gameObject.SetActive(true);
                Announcement.text = "Press SPACE TO RESTART";
            }
        }
        
    }

    public void WinGame()
    {
        Win.text = "YOU WIN";
        Score.text = "Score: " + ((int)(notesPercentage * 100)).ToString() + "%";
        Win.gameObject.SetActive(true);
        raccoonAnim.Play("lose");
        isCompleted = true;
        isWon = true;
        var goal = Instantiate(winGoalPrefab, winGoalSpawnPosition.position, Quaternion.identity);
        goal.GetComponent<GoalCompletion>().roomType = GameManager.RoomType.Racoon;
    }

    public void Init()
    {
        if (isWon)
        {
            return;
        }

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
        GameTime.gameObject.SetActive(true);
        NoteSpawnerPoint = Notes.NoteSpawnerPoint;
        if (isCompleted)
        {
            Notes.enabled = false;
            HitNotes.enabled = false;
        }
    }

    public void DisablePuzzle()
    {

        isEnabled = false;
        targetTime = 40f;
        Announcement.text = "";
        Announcement.gameObject.SetActive(false);
        isCompleted = false;
        GameEnd = false;
        startPlaying = false;
        collectNotes = 0;
        totalNotes = 0;
        Lose.gameObject.SetActive(false);
        Win.gameObject.SetActive(false);
        Score.gameObject.SetActive(false);
        GameTime.gameObject.SetActive(false);
        playerGun.SetActive(false);
    }

    public void EnablePuzzle()
    {
        if (isWon)
        {
            return;
        }

        isEnabled = true;
        Init();
    }
}
