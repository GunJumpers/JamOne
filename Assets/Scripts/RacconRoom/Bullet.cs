
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float life = 3;
    public AK.Wwise.Event collideSFX1 = null;
    public AK.Wwise.Event collideSFX2 = null;
    public AK.Wwise.Event collideSFX3 = null;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {

            other.gameObject.GetComponent<NoteMovement>().shoot = true;
            RaccoonGameData.collectNotes++;
            if (other.gameObject.GetComponent<NoteMovement>().notesTag == 1)
            {
                collideSFX1.Post(gameObject);
            }
            // change later
            else if (other.gameObject.GetComponent<NoteMovement>().notesTag == 2)
            {
                collideSFX2.Post(gameObject);
            }
            else if (other.gameObject.GetComponent<NoteMovement>().notesTag == 3)
            {
                collideSFX3.Post(gameObject);
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}