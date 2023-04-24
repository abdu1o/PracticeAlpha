using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float speed;
    private bool facingRight = true;
    private bool IsRun;
    public Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
}
