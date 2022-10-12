using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEmancipationGrill : MonoBehaviour
{

    public AK.Wwise.Event cubeDestroySound;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DestroyableCube")
        {
            cubeDestroySound.Post(gameObject);
            Destroy(other.transform.parent.gameObject);
        }
    }
}
