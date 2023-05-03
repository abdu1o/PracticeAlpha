using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGrenade : MonoBehaviour
{
    public float timeToExplosion = 5.0f;
    private GameObject smoke;
    private GameObject sparks;

    private bool play = false;

    public Vector3 endPos;
    public Vector2 direction;
    public Vector3 mousePos;

    public void Start()
    {
        smoke = transform.Find("Smoke").gameObject;
        sparks = transform.Find("Sparks").gameObject;
    }

    void Awake()
    {
        Invoke("Explosion", timeToExplosion);
    }

    void FixedUpdate()
    {

        if (play && !smoke.GetComponent<ParticleSystem>().IsAlive() && !sparks.GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(this.gameObject);
        }

        else if ((direction.x < 0 && mousePos.x > transform.position.x) ||
                (direction.x > 0 && mousePos.x < transform.position.x))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            return;
        }

        if (direction.x > 0)
        {
            transform.Rotate(new Vector3(0, 0, -2));
        }

        else
        {
            transform.Rotate(new Vector3(0, 0, 2));
        }
    }

    void Explosion()
    {
        smoke.GetComponent<ParticleSystem>().Play();
        sparks.GetComponent<ParticleSystem>().Play();

        play = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }
}
