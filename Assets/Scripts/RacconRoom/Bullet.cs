
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float life = 3;
    public AK.Wwise.Event collideSFX = null;
    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            other.gameObject.GetComponent<NoteMovement>().shoot = true;
            GameData.collectNotes++;
            collideSFX.Post(gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}