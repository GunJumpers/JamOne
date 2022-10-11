using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEmancipationGrill : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DestroyableCube")
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
