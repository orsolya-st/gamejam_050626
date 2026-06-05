using UnityEngine;

public class DropletMovement : MonoBehaviour
{
<<<<<<< Updated upstream

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
=======
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpingPower = 5f;
    private Vector2 movement;
    private Animator playerAnim; //for animation
    private float xInput;

    //helper variables
    private bool isGrounded = true;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;
>>>>>>> Stashed changes

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
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }

}
