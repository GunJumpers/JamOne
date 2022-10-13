using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public List<GameObject> Notes = new List<GameObject>();
    public float delayTime;
    public float Interval;
    public Transform NoteSpawnerPoint;
    public float NotesNumber;
    public float NotesZ;
    public AK.Wwise.Event EvilLaugh = null;
    void Start()
    {
        Interval = 10f;
        StartCoroutine(SpawnNote());
        StartCoroutine(SpeedUp());
        
    }


    IEnumerator SpawnNote()
    {
        
        if (GameData.startPlaying)
        {
            Instantiate(Notes[0], new Vector3(Random.Range(-1.5f, 1.5f), 1f, NotesZ), Quaternion.identity);
            Instantiate(Notes[1], new Vector3(Random.Range(-1.5f, 1.5f), 1f, NotesZ), Quaternion.identity);
            Instantiate(Notes[2], new Vector3(Random.Range(-1.5f, 1.5f), 1f, NotesZ), Quaternion.identity);
            GameData.totalNotes += 3;

        }
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(SpawnNote());
    }

    IEnumerator SpeedUp()
    {
        if (GameData.startPlaying)
        {
            EvilLaugh.Post(gameObject);
            Notes[0].GetComponent<NoteMovement>().beatTempo += 0.25f;
            Notes[1].GetComponent<NoteMovement>().beatTempo += 0.3f;
            Notes[2].GetComponent<NoteMovement>().beatTempo += 0.5f;
        }
        yield return new WaitForSeconds(Interval);
        StartCoroutine(SpeedUp());
    }

}