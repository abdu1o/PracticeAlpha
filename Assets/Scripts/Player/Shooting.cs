using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    public int shootDirection; //use ONLY -1 or 1
    public float spriteDirection;

    public float bulletLifetime = 2f; // bullet lifetime

    private float reloadTime;
    public float shootSpeed;

    void Update()
    {
        if (reloadTime <= 0)
        {
            if(Input.GetButtonDown("Fire1")) //mouse 1
            {
                Shoot();
                reloadTime = shootSpeed;
            }
        }  
        else
        {
            reloadTime -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.Rotate(0f, 0f, spriteDirection);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootDirection * firePoint.up * bulletForce, ForceMode2D.Impulse);

        Destroy(bullet, bulletLifetime);
    }
}
