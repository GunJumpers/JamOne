using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public List<GameObject> Notes = new List<GameObject>();
    public float delayTime;
    public float Interval;
    public Transform NoteSpawnerPoint;
    public GameData GameManager;
    public float NotesNumber;
    public float NotesZ;
    public bool hasStarted;
    public AK.Wwise.Event EvilLaugh = null;
    void Start()
    {
        Interval = 10f;
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
            EvilLaugh.Post(gameObject);
            for (int i = 0; i < NotesNumber; i++)
            {
                Instantiate(Notes[0], new Vector3(Random.Range(-2f, 2f), 1f, NotesZ), Quaternion.identity);
                Instantiate(Notes[1], new Vector3(Random.Range(-2f, 2f), 1f, NotesZ), Quaternion.identity);
                Instantiate(Notes[2], new Vector3(Random.Range(-2f, 2f), 1f, NotesZ), Quaternion.identity);
                GameManager.totalNotes+= 3;
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
            if (NotesNumber <= 5)
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