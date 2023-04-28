using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float speed;
    private bool facingRight = true;
    private bool IsRun;
    public Rigidbody2D rb;
    private Animator anim;

    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (mousePosition.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (mousePosition.x < transform.position.x && facingRight)
        {
            Flip();
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 moveInput = new Vector2(horizontalInput, verticalInput);
        Vector2 moveVelocity = moveInput.normalized * speed;

        if (moveInput.x == 0 && moveInput.y == 0)
        {
            anim.SetBool("IsRun", false);
        }
        else
        {
            anim.SetBool("IsRun", true);
        }

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.name == "Sparks")
        {
            GameObject grenade = GetComponentInParent<ThrowGrenade>().grenadeCreated;
            ParticleSystem particles = grenade.transform.Find("Sparks").GetComponent<ParticleSystem>();

            int numCollisionEvents = particles.GetCollisionEvents(gameObject, collisionEvents);

            Texture2D newTexture = new Texture2D(transform.Find("Body").GetComponent<SpriteRenderer>().sprite.texture.width,
                                                    transform.Find("Body").GetComponent<SpriteRenderer>().sprite.texture.height);

            for (int y = 0; y < transform.Find("Body").GetComponent<SpriteRenderer>().sprite.texture.height; y++)
            {
                for (int x = 0; x < transform.Find("Body").GetComponent<SpriteRenderer>().sprite.texture.width; x++)
                {
                    newTexture.SetPixel(x, y, transform.Find("Body").GetComponent<SpriteRenderer>().sprite.texture.GetPixel(x, y));
                }
            }

            Sprite sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
            transform.Find("Body").GetComponent<SpriteRenderer>().sprite = sprite;

            for (int i = 0; i < numCollisionEvents; i++)
            {
                StatsControl stats = GetComponent<StatsControl>();
                stats.TakeDamage(5);

                Vector2 collisionPos = transform.Find("Body").transform.InverseTransformVector(collisionEvents[i].intersection);

                int x = (int)(collisionPos.x * 100);
                int y = (int)(collisionPos.y * 100);

                int r = 8;
                int step = 4;
                int yCirc;
                int xCirc = r / 2;

                for (yCirc = Random.Range(2, r + 5); yCirc <= r; yCirc++)
                {
                    Color currentColor = newTexture.GetPixel(x + xCirc, y + yCirc);
                    Color newColor = new Color(0, 0, 0, 0);

                    if (currentColor.Equals(Color.black))
                    {
                        newColor = new Color(0, 0, 0, 0);
                    }

                    else
                    {
                        newColor = Color.black;
                    }

                    for (int j = 0; j <= step + Random.Range(0, 5); j++)
                    {
                        newTexture.SetPixel(x + xCirc - j, y + yCirc, newColor);
                    }

                    for (int j = 0; j <= step + Random.Range(0, 5); j++)
                    {
                        newTexture.SetPixel(x + xCirc + j, y + yCirc, newColor);
                    }

                }
            }

            newTexture.Apply();
        }
    }
}
