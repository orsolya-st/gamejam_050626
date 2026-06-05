using UnityEngine;
using System.Collections;

public class DropletMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // helper variables
    private bool isGrounded = true;
    private float facingDirection = 1f;

    // movement
    private Vector2 movement;
    public float jumpingPower = 5f;
    public float moveSpeed = 5f;

    // dashing
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");

        if (movement.x < 0) facingDirection = -1;
        if (movement.x > 0) facingDirection = 1;

        if (isGrounded &&
            (Input.GetKeyDown(KeyCode.Space) ||
             Input.GetKeyDown(KeyCode.UpArrow) ||
             Input.GetKeyDown(KeyCode.W)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        if (tr != null)
        {
            tr.emitting = true;
        }

        yield return new WaitForSeconds(dashingTime);

        if (tr != null)
        {
            tr.emitting = false;
        }

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        isGrounded = false;
    }
}