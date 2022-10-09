using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawner;
    public float bulletSpeed;
    public AudioSource shootSFX;
    private bool hasStarted = false;

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
            }
        }

        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                shootSFX.Play();
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawner.forward * bulletSpeed;
            }
        }
    }
}
