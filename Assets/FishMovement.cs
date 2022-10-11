using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public Transform playerPoint;
    public float Speed;
    public float stopDistance;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
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
}
