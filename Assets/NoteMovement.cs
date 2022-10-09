using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float life = 20;
    public bool shoot = false;
    public float beatTempo;
    private bool started = false;
    private void Awake()
    {
        if (!shoot)
        {
            Destroy(gameObject, life);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(beatTempo);
        /*if (!started)
        {
            if (Input.anyKeyDown)
            {
               started = true;
            }
        }

        else
        {*/
            transform.position += new Vector3(0f, 0f, beatTempo * Time.deltaTime);

    }
}
