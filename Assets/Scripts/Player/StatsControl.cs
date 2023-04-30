using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsControl : MonoBehaviour
{
    public int max_health = 100;
    public int health; 

    public HealthBar health_bar;
    private Rigidbody2D rb;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        health = max_health;
        health_bar.SetMaxHealth(max_health);
    }

    void Update() 
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            rb.isKinematic = true;
            TakeDamage(5);
            Destroy(collision.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        health_bar.SetHealth(health);
    }
}
