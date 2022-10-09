using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public List<GameObject> Notes = new List<GameObject>();
    public float delayTime = 1;
    public Transform NoteSpawnerPoint;
    void Start()
    {
       StartCoroutine(SpawnNote());
    }

    IEnumerator SpawnNote()
    {
        Instantiate(Notes[0], new Vector3(Random.Range(-6f, -2.5f), 1f, NoteSpawnerPoint.position.z), Quaternion.identity);
        Instantiate(Notes[1], new Vector3(Random.Range(-2f, 3f), 1f, NoteSpawnerPoint.position.z), Quaternion.identity);
        Instantiate(Notes[2], new Vector3(Random.Range(3.5f, 6f), 1f, NoteSpawnerPoint.position.z), Quaternion.identity);
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(SpawnNote());
    }

    public void changeNoteTempo(int i, float tempo)
    {
        Notes[i].GetComponent<NoteMovement>().beatTempo = tempo;
    }
}
