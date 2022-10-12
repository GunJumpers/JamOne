using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject spawnedCubePrefab;
    public GameObject spawnedCube;
    public Transform summonPosition;

    public AK.Wwise.Event spawnSound;
    public AK.Wwise.Event cubeDestroySound;
    public void SpawnCube()
    {

        StartCoroutine(NukeCube());
        
    }

    IEnumerator NukeCube()
    {
        if (spawnedCube != null)
        {
            spawnedCube.transform.position = new Vector3(50, 50, 50);
            yield return new WaitForSeconds(0.1f);
            Destroy(spawnedCube);
        }
        yield return new WaitForSeconds(0.2f);
        spawnedCube = Instantiate(spawnedCubePrefab, summonPosition.position, Quaternion.identity);

    }
}
