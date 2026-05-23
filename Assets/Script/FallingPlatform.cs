using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Fall")]
    public float fallDelay = 1.5f;

    [Header("Shake")]
    public float shakeAmount = 0.05f;

    private Rigidbody2D rb;

    private bool isFalling;

    private bool isShaking;

    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType =
            RigidbodyType2D.Kinematic;

        startPos = transform.position;
    }

    void Update()
    {
        // shake effect
        if (isShaking)
        {
            transform.position =
                startPos +
                (Vector3)Random.insideUnitCircle
                * shakeAmount;
        }
    }

    private void OnCollisionEnter2D(
        Collision2D collision
    )
    {
        if (collision.gameObject
            .CompareTag("Player"))
        {
            if (!isFalling)
            {
                isFalling = true;

                isShaking = true;

                Invoke(
                    nameof(Fall),
                    fallDelay
                );
            }
        }
    }

    void Fall()
    {
        isShaking = false;

        transform.position = startPos;

        rb.bodyType =
            RigidbodyType2D.Dynamic;

        Destroy(gameObject, 3f);
    }
}