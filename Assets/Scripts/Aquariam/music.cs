using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            other.GetComponent<FishMovement>().isSoothed = true;
            GameStat.Instance.CheckPuzzleComplete();
            Destroy(gameObject);
        }
    }
}
