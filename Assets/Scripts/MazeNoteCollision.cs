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
        }
        if (collision.gameObject.CompareTag("shrinkDetect"))
        {
            //shrink player or unshrink
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Debug.Log("shrunk");
            //soundObjects.SetActive(true);
            //shrinkpanel.SetActive(false);
            //unShrinkPanel.SetActive(true);
        }
        if (collision.gameObject.CompareTag("unshrinkDetect"))
        {
            //shrink player or unshrink
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Debug.Log("unshrunk");
            //unShrinkPanel.SetActive(false);
            //shrinkpanel.SetActive(true);
        }
        if (collectNumber == 3)
        {
            Debug.Log("YOU WIN");
            winObject.SetActive(true);
            mazeBlocks.SetActive(false);
            exWall.SetActive(false);
            unShrinkPanel.SetActive(true);
            shrinkpanel.SetActive(false);
        }
    }
}
