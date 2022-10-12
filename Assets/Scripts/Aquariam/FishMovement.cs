using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public Transform playerPoint;
    public float Speed;
    public float MovementInterval;
    public float stopDistance;
    private Vector3 startPosition;
    private Rigidbody rb;
    private float dirX;
    private bool faceRight;
    private Vector3 localScale;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        rb = GetComponent<Rigidbody>();
        dirX = -1f;
        StartCoroutine(FishChangeDirections());
        MovementInterval = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
       // Fish move right and left
       if (rb)
        {
            rb.velocity = new Vector3(dirX * Speed, rb.velocity.y, rb.velocity.z);
        }
       

        if (GameStat.playerIsEntered)
        {
            float dist = Vector3.Distance(playerPoint.position, transform.position);
            if (GameStat.isSoothed)
            {
                Destroy(rb);
            }
            else {
                if (dist > stopDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerPoint.position, Speed * Time.deltaTime);
                }
            }

        }
    }

    public void FishReset()
    {
        gameObject.transform.position = startPosition;
        GameStat.isSoothed = false;
        gameObject.AddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Debug.Log("FISH RESET");
    }

    IEnumerator FishChangeDirections ()
    {
        dirX *= -1f;
        //faceRight = !faceRight;
        //localScale.x *= -1;
        //transform.localScale = localScale;
        yield return new WaitForSeconds(MovementInterval);
        StartCoroutine(FishChangeDirections());
;    }
}
