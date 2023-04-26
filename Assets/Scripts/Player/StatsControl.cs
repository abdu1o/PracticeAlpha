using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsControl : MonoBehaviour
{
    public int max_health = 100;
    public int health; 

    public HealthBar health_bar;
    public UnityEngine.Rendering.Universal.Light2D light2D;

    void Start() 
    {
        health = max_health;
        health_bar.SetMaxHealth(max_health);
    }

    //just for tests
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        if(health <= 0)
        {
            Destroy(light2D);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        health_bar.SetHealth(health);
    }
}
