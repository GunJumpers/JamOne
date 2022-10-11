using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public bool isBubbled = false;
    public Transform MusicSpawner;
    public GameObject MusicPrefab;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isBubbled)
        //{
            if (Input.GetMouseButtonDown(0))
            {
                GameObject Music = Instantiate(MusicPrefab, MusicSpawner.position,MusicSpawner.rotation);
                Music.GetComponent<Rigidbody>().velocity = MusicSpawner.forward * speed;
            }
        //}
    }

    private void OnTriggerEnter (Collider other) 
    {
        if (other.gameObject.CompareTag("Water"))
        {
            GameStat.playerIsEntered = true;
            if (!isBubbled)
            {
                Debug.Log("Radio Broken");
                GameStat.GameOver = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            GameStat.playerIsEntered = false;
        }

    }
}
