using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DropletMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D characterCollider;

    // helper variables
    private bool isGrounded = false;
    private float facingDirection = 1f;
    private bool isDropping = false;
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
        characterCollider = GetComponent<Collider2D>();
    }

    [System.Obsolete]
    void Update()
    {
        if (isDashing || isDropping)
        {
            return;
        }

        //dashing
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x < 0) facingDirection = -1;
        if (movement.x > 0) facingDirection = 1;

        //Jumping
        if (isGrounded &&
            (Input.GetKeyDown(KeyCode.Space) ||
             Input.GetKeyDown(KeyCode.UpArrow) ||
             Input.GetKeyDown(KeyCode.W))
             && !isDropping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        //Jump animation
        if (rb.linearVelocityY > 1)
        {
            animator.SetBool("Jumping", true);
        } else
        {
            animator.SetBool("Jumping", false);
        }
        if (rb.linearVelocityY < -0.5 && !isGrounded)
        {
            animator.SetBool("Falling", true);
        } else
        {
            animator.SetBool("Falling", false);
        }

        //Walk animation
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

        //Dashing
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        
        //dropdown
        if (isGrounded && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
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

        //wind
        float windForce = 0f;
        if (WindManager.Instance != null)
        {
            windForce = WindManager.Instance.CurrentWindForce;
        }
        rb.linearVelocity = new Vector2(movement.x * moveSpeed + windForce, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Hole
        if (other.CompareTag("Hole"))
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
            Destroy(gameObject);
            Debug.Log("Fell in hole!");
        }

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
        /*if (col.collider.CompareTag("Platform"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }*/

        CheckGrounded(col);
    }

    private void CheckGrounded(Collision2D col)
    {
        if (col.collider.CompareTag("Platform"))
        {
            // Verify the character is actually standing on top of the platform
            for (int i = 0; i < col.contactCount; i++)
            {
                if (col.GetContact(i).normal.y >= 0.5f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }


    private void DropDown()
    {
        //when "s" or "down key" is pressed turn of/disable collider;
        //move object slowly down (1 seconds, move down the height of the collision box)
        //after that enable collider again

        if (isGrounded && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            animator.SetBool("Dropping", true);
            animator.SetBool("WalkingRight", false);
            animator.SetBool("WalkingLeft", false);
            StartCoroutine(DropRoutine());
        }
    }

    private IEnumerator DropRoutine()
    {
        isDropping = true; // Lock out other actions immediately
        rb.linearVelocityX = 0; // Strip horizontal momentum
        
        characterCollider.isTrigger = true;
            
        float duration = 1f;
        float elapsed = 0f; // measure time
            
        // move down the height of the collider
        float dropDistance = characterCollider.bounds.size.y;
        float speed = dropDistance / duration;

        while (elapsed < duration)
        {
            rb.gravityScale = 0;
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            elapsed += Time.deltaTime; // update time
            yield return null;
        }

        // enable collider --> not a trigger anymore
        rb.gravityScale = 1;
        characterCollider.isTrigger = false;
        isDropping = false;
        isGrounded = false;
        animator.SetBool("Dropping", false);
    }
}