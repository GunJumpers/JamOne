using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNoteCollision : MonoBehaviour
{
    public int collectNumber = 0;
    public GameObject winObject;
    public GameObject mazeBlocks;
    public GameObject exWall;
    public GameObject unShrinkPanel;
    public GameObject shrinkpanel;
    public GameObject soundObjects;
    public List<AK.Wwise.Event> noteStartEvents;
    public List<AK.Wwise.Event> noteStopEvents;
    public bool isWon;

    [Header("Win Objects")]
    public GameObject winGoalPrefab;
    public Transform winGoalSpawnLocation;

    private void Start()
    {
        winObject.SetActive(false);
        mazeBlocks.SetActive(true);
        exWall.SetActive(true);
        unShrinkPanel.SetActive(false);
        //soundObjects.SetActive(false);
        shrinkpanel.SetActive(true);   

    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("mazeMusicNote"))
        {
            Destroy(collision.gameObject);
            collectNumber++;
            Debug.Log("collided");
            if (collectNumber == 3)
            {
                WinGame();
            }
        }
        
        
    }

    public void ShrinkPlayer()
    {
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Debug.Log("shrunk");
    }

    public void GrowPlayer()
    {
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Debug.Log("unshrunk");
    }

    public void WinGame()
    {
        Debug.Log("YOU WIN");
        //winObject.SetActive(true);
        mazeBlocks.SetActive(false);
        exWall.SetActive(false);
        unShrinkPanel.SetActive(true);
        shrinkpanel.SetActive(false);
        isWon = true;
        var goal = Instantiate(winGoalPrefab, winGoalSpawnLocation.position, Quaternion.identity);
        goal.GetComponent<GoalCompletion>().roomType = GameManager.RoomType.Maze;
    }

    public void EnablePuzzle()
    {
        if (isWon)
        {
            return;
        }
        for (int i = 0; i < soundObjects.transform.childCount; i++)
        {
            noteStartEvents[i].Post(soundObjects.transform.GetChild(i).gameObject);


        }
    }

    public void DisablePuzzle()
    {
        for (int i = 0; i < soundObjects.transform.childCount; i++)
        {
            noteStopEvents[i].Post(soundObjects.transform.GetChild(i).gameObject);


        }
    }
}
