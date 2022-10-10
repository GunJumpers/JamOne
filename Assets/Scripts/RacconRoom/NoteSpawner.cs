using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public List<GameObject> Notes = new List<GameObject>();
    public float delayTime;
    public float Interval;
    public Transform NoteSpawnerPoint;
    public GameManage GameManager;
    public float NotesNumber;
    public float NotesZ;
    public bool hasStarted;
    public Conductor Music;
    void Start()
    {
        Interval = Music.musicSource.clip.length / 8;
        StartCoroutine(SpawnNote());
        StartCoroutine(SpeedUp());
    }

    public void changeNoteTempo(int i, float tempo)
    {
        Notes[i].GetComponent<NoteMovement>().beatTempo = tempo;
        NotesNumber = tempo;
        NotesZ = NoteSpawnerPoint.position.z;
    }

    IEnumerator SpawnNote()
    {
        if (hasStarted)
        {
            
            if (NotesNumber == 1 || NotesNumber == 2)
            {
                for (int i = 0; i < NotesNumber; i++)
                {
                    Instantiate(Notes[0], new Vector3(Random.Range(-3f, 3f), 1f, NotesZ), Quaternion.identity);
                    NotesZ += 0.5f;
                    GameManager.totalNotes++;
                }
            }
            else if (NotesNumber == 3.5 || NotesNumber == 2.5)
            {
                for (int i = 0; i < NotesNumber + 0.5; i++)
                {
                    Instantiate(Notes[1], new Vector3(Random.Range(-3f, 3f), 1f, NotesZ), Quaternion.identity);
                    NotesZ += 0.5f;
                    GameManager.totalNotes++;
                }
            }
            else if (NotesNumber == 3 || NotesNumber == 4.5)
            {
                for (int i = 0; i < NotesNumber; i++)
                {
                    Instantiate(Notes[2], new Vector3(Random.Range(-3f, 3f), 1f, NotesZ), Quaternion.identity);
                    NotesZ += 0.5f;
                    GameManager.totalNotes++;
                }
            }
        }
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(SpawnNote());
    }

    IEnumerator SpeedUp()
    {
        if (hasStarted)
        {
            //Debug.Log("speed up");
            if (NotesNumber <= 6)
            {
                NotesNumber += 0.5f;
            }
            if (delayTime >= 1)
            {
                delayTime -= 0.25f;
            }
        }
        yield return new WaitForSeconds(Interval);
        StartCoroutine(SpeedUp());
    }

}
