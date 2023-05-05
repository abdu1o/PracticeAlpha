using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    public Transform player;
    public Transform turret;

    public float rotationSpeed = 5f;
    public float shootingDistance = 10f;

    public float bulletForce = 20f;
    public float bulletLifetime = 2f;
    public float shootingDelay = 0.4f;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private float angle;

    private bool canShoot = true;

    private Animator animator;
    private const string shootingAnimationTrigger = "Shoot";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 direction = player.position - turret.position;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        turret.rotation = Quaternion.Slerp(turret.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (direction.magnitude <= shootingDistance && canShoot)
        {
            canShoot = false;
            Invoke("Shoot", shootingDelay);
        }
    }

     void Shoot()
    {
        animator.SetTrigger(shootingAnimationTrigger);
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        bullet.transform.Rotate(0f, 0f, 270f);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(-bulletSpawnPoint.right * bulletForce, ForceMode2D.Impulse);

        Destroy(bullet, bulletLifetime);

        Invoke("ResetShoot", shootingDelay);
    }

    void ResetShoot()
    {
        canShoot = true;
    }
}