using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float life = 20;
    public bool shoot = false;
    public float beatTempo;
    private void Awake()
    {
        if (!shoot)
        {
            Destroy(gameObject, life);
        }
    }
    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(0f, 0f, beatTempo * Time.deltaTime);

    }
}