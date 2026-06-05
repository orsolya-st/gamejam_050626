using UnityEngine;

public class DropletMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // movement
    [Header("Movement")]
    private Vector2 movement;
    private float horizontal;
    private bool isFacingRight = true;
    public float moveSpeed = 5f;
    public float jumpingPower = 16f;

    // dashing
    [Header("dashing")]
    private bool isGrounded;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    //animation
    private Animator playerAnim;
    private float xInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //------------dashing------------
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
