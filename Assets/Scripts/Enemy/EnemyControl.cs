using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform player;
    private bool facingRight = true;
    public float moveSpeed = 3f;
    public float maxDistance = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Transform flippedTransform;
    private Transform flippedPart;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
 
        // Найти дочерний объект "flippedPart"
        flippedPart = transform.Find("flippedPart");
    }

    private void FixedUpdate()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, player.position) > maxDistance)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }

        if (direction.x > 0 && facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = flippedPart.localScale;
        Scaler.x *= -1;
        flippedPart.localScale = Scaler;
    }


}
