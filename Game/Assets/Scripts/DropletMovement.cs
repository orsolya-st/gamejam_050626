using UnityEngine;
using System.Collections;

public class DropletMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D characterCollider;

    // helper variables
    private bool isGrounded = true;
    private float facingDirection = 1f;
    private bool isDropping = false;
    // private float fallAngleThreshold = -45f;
    private float fallTimer;
    private bool isFalling;
    public float maxFallTime = 0.5f;

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
    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    [System.Obsolete]
    void Update()
    {

        //reset falltimer when start the jump
        // if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        // {
        //     fallTimer = 0f; //jump resets falltime
        // }

        //fall damage
        // if (isGrounded && fallTimer > 0.6f)
        // {
        //     Destroy(gameObject); //TODO
        // }

        //handle jump/midair variables
        // if (isGrounded)
        // {
        //     isFalling = false;
        // } 
        // else
        // {
        //     HandleMidairLogic();
        // }
        
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
             Input.GetKeyDown(KeyCode.W))
             && !isDropping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
        if (movement.x > 0) {
            animator.SetBool("WalkingRight", true);
            animator.SetBool("WalkingLeft", false);
        } else if (movement.x <0 && movement.x != 0)
        {
            animator.SetBool("WalkingRight", false);
            animator.SetBool("WalkingLeft", true);
        } else
        {
            animator.SetBool("WalkingRight", false);
            animator.SetBool("WalkingLeft", false);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Border"))
        {
            Destroy(gameObject); //for now it destroys the droplet
            Debug.Log("Droplet dropped down, animation, game end!");
        }*/

        //Hole
        if (other.CompareTag("Hole"))
        {
            Destroy(gameObject);
            Debug.Log("Fell in hole!");
        }

    }

    // [System.Obsolete]
    // private void HandleMidairLogic()
    // {
    //     if (rb.linearVelocity.y >= 0)
    //     {
    //         isFalling = false;
    //         fallTimer = 0f;
    //         return;
    //     }
    //     
    //     float movementAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
    //     //Degrees: 0 - right, 90 - up, -90 - down --> downwards is beteen -90 and -180
    //     //Standard treshhold (degree) = -45 --> between -45 and -135 degrees is falling (instead of jumping)
    //     bool fallingDown = (movementAngle < fallAngleThreshold && movementAngle > (-180f - fallAngleThreshold));
    //
    //     if (fallingDown || rb.velocity.x == 0)
    //     {
    //         isFalling = true;
    //         fallTimer += Time.deltaTime;
    //         Debug.Log($"Fall time: {fallTimer:F2} seconds");
    //     }
    // }

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
        isGrounded = false;
    }
}