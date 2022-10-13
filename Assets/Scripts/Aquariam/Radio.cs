using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public bool isBubbled = false;
    public Transform MusicSpawner;
    public GameObject bubbleFX;
    public GameObject MusicPrefab;
    public List<GameObject> Bubbles = new List<GameObject>();
    private float speed = 3;
    private float interval = 10;
    public AK.Wwise.Event bubbleSFX = null;
    private AK.Wwise.Event radioFizzleSFX = null;
    // Start is called before the first frame update
    void Start()
    {
        bubbleFX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (bubbleFX.activeInHierarchy)
        {
        }
        if (GameStat.radioUsed)
        {
            if (Input.GetMouseButtonDown(0) && isBubbled)
            {
                GameObject Music = Instantiate(MusicPrefab, MusicSpawner.position,MusicSpawner.rotation);
                //radioFizzleSFX.Post(gameObject);
                Music.GetComponent<Rigidbody>().velocity = MusicSpawner.forward * speed;
            }
        }

        checkGameOver();
    }


    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("Fish"))
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            GameStat.playerIsEntered = false;
            bubbleFX.SetActive(false);
            interval = 0;
            Debug.Log("LAND");
        }
        if (other.gameObject.CompareTag("Water"))
        {
            GameStat.playerIsEntered = true;
            Debug.Log("WATER");
        }

        if (other.gameObject.CompareTag("AquariumGoal"))
        {
            Destroy(other.gameObject);
            GameStat.isCompleted = true;
            GameStat.Instance.FinishLevel();
            
        }
        // Collide with bubble
        if (other.gameObject.CompareTag("Note"))
        {
            other.gameObject.SetActive(false);
            isBubbled = true;
            interval += 10f;
            StartCoroutine(DestoryBubbleFx());
            //Debug.Log("Interval: " + interval);
            //Debug.Log("Bubble: " + isBubbled);
            bubbleFX.SetActive (true);
            //bubbleSFX.Post(gameObject);
        }
        if (other.gameObject.CompareTag("Fish"))
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

    }

    IEnumerator DestoryBubbleFx()
    {
        yield return new WaitForSeconds(interval);
        bubbleFX.SetActive(false);
        isBubbled = false;
        
    }

    private void checkGameOver()
    {
        
        if (!isBubbled && !GameStat.isCompleted && GameStat.radioUsed && GameStat.playerIsEntered)
        {
            interval = 0;
            bubbleFX.SetActive(false);
            for (int i = 0; i< 3; i++)
            {
                Bubbles[i].gameObject.SetActive(true);
            }
            GameStat.GameOver = true;

        }
    }
}
