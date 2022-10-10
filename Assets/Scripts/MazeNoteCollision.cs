using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNoteCollision : MonoBehaviour
{
    public int collectNumber = 0;
    public AudioClip ButtonHit;
    public GameObject winObject;

    private void Start()
    {
        winObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("mazeMusicNote"))
        {
            AudioSource.PlayClipAtPoint(ButtonHit, transform.position);
            Destroy(collision.gameObject);
            collectNumber++;
            Debug.Log("collided");
        }

        if (collectNumber == 3)
        {
            Debug.Log("YOU WIN");
            winObject.SetActive(true);
        }
    }

}
