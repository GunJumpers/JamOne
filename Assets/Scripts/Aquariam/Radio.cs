using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public bool isBubbled = false;
    public Transform MusicSpawner;
    public GameObject MusicPrefab;
    public GameObject bubbleFX;
    public List<GameObject> Bubbles = new List<GameObject>();
    public float speed = 3;
    private float interval = 10;
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
            StartCoroutine(DestoryBubbleFx());
        }
        if (GameStat.radioUsed)
        {
            if (Input.GetMouseButtonDown(0) && isBubbled)
            {
                GameObject Music = Instantiate(MusicPrefab, MusicSpawner.position,MusicSpawner.rotation);
                Music.GetComponent<Rigidbody>().velocity = MusicSpawner.forward * speed;
            }
        }

        checkGameOver();
    }


    private void OnTriggerExit(Collider other)
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
        if (other.gameObject.CompareTag("Fish"))
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyableCube"))
        {
            Destroy(other.gameObject);
            GameStat.isCompleted = true;
            
        }
        // Collide with bubble
        if (other.gameObject.CompareTag("Note"))
        {
            other.gameObject.SetActive(false);
            isBubbled = true;
            interval += 10f;
            Debug.Log("Interval: " + interval);
            Debug.Log("Bubble: " + isBubbled);
            bubbleFX.SetActive (true);
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
        
        if (!isBubbled && GameStat.radioUsed && GameStat.playerIsEntered)
        {
            Debug.Log("Radio Broken");
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
