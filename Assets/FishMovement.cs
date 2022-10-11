using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public Transform playerPoint;
    public float Speed;
    public float stopDistance;
    private Vector3 startPosition;
    private Rigidbody rb;
    private float dirX;
    private bool faceRight;
    private float moveDistance;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        rb = GetComponent<Rigidbody>();
        dirX = -1f;
    }

    // Update is called once per frame
    void Update()
    {
       // Fish move right and left

        if (GameStat.playerIsEntered)
        {
            Debug.Log("Player Entered");
            float dist = Vector3.Distance(playerPoint.position, transform.position);
            if (dist > stopDistance)
            {
                if (!GameStat.isSoothed)
                {
                    Debug.Log("MOVE");
                    transform.position = Vector3.MoveTowards(transform.position, playerPoint.position, Speed*Time.deltaTime);
                }
            }

            else
            {
                Debug.Log("STOP");
            }
        }
    }

    public void FishReset()
    {
        gameObject.transform.position = startPosition;
        Debug.Log("FISH RESET");
    }

    IEnumerator FishChangeDirections ()
    {
       
    }
}
