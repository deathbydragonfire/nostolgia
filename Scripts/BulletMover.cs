using UnityEngine;

public class BulletMover : MonoBehaviour
{
    // The speed of the bullet
    public float speed = 10.0f;

    // The lifetime of the bullet, in seconds
    public float lifetime = 5.0f;

    // A timer to keep track of the elapsed time
    private float timer = 0.0f;

    void Start()
    {
        // Get the Rigidbody2D component of the bullet
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Set the velocity of the bullet based on its local scale
        if (transform.localScale.x < 0)
        {
            // If the local scale is negative, shoot to the left
            rb.velocity = new Vector2(-speed, 0);
        }
        else
        {
            // If the local scale is positive, shoot to the right
            rb.velocity = new Vector2(speed, 0);
        }
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the bullet has exceeded its lifetime
        if (timer >= lifetime)
        {
            // If the lifetime has been exceeded, destroy the bullet
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // When the bullet becomes invisible, destroy it
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When the bullet collides with another object, destroy it
        Destroy(gameObject);
    }
}
