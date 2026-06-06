using UnityEngine;
using System.Collections;

public class DropletMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // helper variables
    private bool isGrounded = true;
    private float facingDirection = 1f;
    private bool isDropping = false;

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
        
        //dashing
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
        
        //dropdown
        if (!isGrounded){return;}
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            DropDown();
        }

    }

    private void FixedUpdate()
    {
        if (isDashing || isDropping)
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

        rb.linearVelocity = new Vector2(facingDirection * dashingPower, 0f);

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

    private void DropDown()
    {
        //when "s" or "down key" is pressed turn of/disable collider;
        //move object slowly down (1 seconds, move down the height of the collision box)
        //after that enable collider again

        if (isGrounded && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            StartCoroutine(DropRoutine());
        }
    }

    private IEnumerator DropRoutine()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        playerCollider.isTrigger = true;
            
        float duration = 1f;
        float elapsed = 0f; //measure time
            
        //move down the height of the collider
        float dropDistance = playerCollider.bounds.size.y;
        float speed = dropDistance/duration;

        while (elapsed < duration)
        {
            isDropping = true;
            rb.gravityScale = 0;
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            elapsed += Time.deltaTime; //update time
            yield return null;
        }

        //enable collider --> not a trigger anymore
        rb.gravityScale = 1;
        playerCollider.isTrigger = false;
        isDropping = false;

    }
}