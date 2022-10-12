using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNoteCollision : MonoBehaviour
{
    public int collectNumber = 0;
    public AudioClip ButtonHit;
    public GameObject winObject;
    public GameObject mazeBlocks;
    public GameObject exWall;
    public AK.Wwise.Event musicNote1 = null;
    public AK.Wwise.Event musicNote2 = null;
    public AK.Wwise.Event musicNote3 = null;

    private void Start()
    {
        winObject.SetActive(false);
        mazeBlocks.SetActive(true);
        exWall.SetActive(true);
        musicNote1.Post(gameObject);
        musicNote2.Post(gameObject);
        musicNote3.Post(gameObject);
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("mazeMusicNote"))
        {
            AudioSource.PlayClipAtPoint(ButtonHit, transform.position);
            Destroy(collision.gameObject);
            musicNote1.Stop(collision.gameObject);
            musicNote2.Stop(collision.gameObject);
            musicNote3.Stop(collision.gameObject);
            collectNumber++;
            Debug.Log("collided");
        }

        if (collision.gameObject.CompareTag("shrinkDetect"))
        {
            //shrink player or unshrink
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Debug.Log("shrunk");
        }

        if (collectNumber == 3)
        {
            Debug.Log("YOU WIN");
            winObject.SetActive(true);
            mazeBlocks.SetActive(false);
            exWall.SetActive(false);
        }
    }
}
