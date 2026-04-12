using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 7f;
    public float laneDistance = 2f;
    public float laneSmoothSpeed = 10f;
    public float forwardSpeed = 5f; 
    public ScoreManager scoreManager;


    private Rigidbody2D rb;
    private bool isGrounded;

    private int currentLane = 1;

    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // START DI TENGAH
        rb.position = new Vector2(0, rb.position.y);
        currentLane = 1;
    }

    void Update()
    {
        // kiri
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentLane--;
            if (currentLane < 0) currentLane = 0;
        }

        // kanan
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentLane++;
            if (currentLane > 2) currentLane = 2;
        }

        // target lane
        targetPosition = new Vector3(
            (currentLane - 1) * laneDistance,
            transform.position.y,
            0
        );

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // GERAK MAJU 
        rb.velocity = new Vector2(forwardSpeed, rb.velocity.y);

        // lane movement 
        Vector3 newPos = Vector3.Lerp(transform.position, targetPosition, laneSmoothSpeed * Time.fixedDeltaTime);
        transform.position = new Vector3(newPos.x, transform.position.y, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
{
    scoreManager.GameOver();
}

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
