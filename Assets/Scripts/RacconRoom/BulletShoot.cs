using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawner;
    public float bulletSpeed;
    public AudioSource shootSFX;
    public GameData GameManager;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !GameData.GameEnd)
        {
            shootSFX.Play();
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawner.forward * bulletSpeed;

        }
    }
}