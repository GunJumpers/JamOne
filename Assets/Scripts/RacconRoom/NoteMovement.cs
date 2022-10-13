using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float life = 10;
    public bool shoot = false;
    public float beatTempo;
    public int notesTag;
    private void Awake()
    {
        if (!shoot)
        {
            Destroy(gameObject, life);
        }
    }

    public void Start()
    {
        beatTempo = 1.5f;
    }
    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(0f, 0f, beatTempo * Time.deltaTime);

    }

}