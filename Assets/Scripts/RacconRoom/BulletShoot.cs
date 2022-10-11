using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawner;
    public float bulletSpeed;
    public GameData GameManager;
    public AK.Wwise.Event shootSFX = null;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !GameData.GameEnd)
        {
            shootSFX.Post(gameObject);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawner.forward * bulletSpeed;

        }
    }
}