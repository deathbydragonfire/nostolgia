using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    GameObject player;

    // The distance at which the projectile will start homing in on the player
    public float homeInDistance = 10.0f;

    // The speed at which the projectile will fly towards the player's location
    public float flyTowardsSpeed = 5.0f;

    // The speed at which the projectile will home in on the player
    public float homeInSpeed = 10.0f;

    // The delay before the projectile starts flying towards the player's location
    public float flyTowardsDelay = 1.0f;

    // The initial position of the player
    private Vector3 playerPos;

    // Flag indicating whether the projectile is homing in on the player
    private bool isHomingIn = false;

    public Animator anim;

    private float timer = 0f;

    private Vector3 direction;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // Save the initial position of the player
        playerPos = player.transform.position;
        direction = playerPos - transform.position;
        // Wait for the flyTowardsDelay before starting to fly towards the player's location
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > flyTowardsDelay)
        {
            // Check if the projectile is homing in on the player
            if (isHomingIn)
            {
                // Calculate the direction towards the player
                Vector3 direction = player.transform.position - transform.position;

                // Normalize the direction
                direction.Normalize();

                // Move the projectile towards the player
                transform.position += direction * homeInSpeed * Time.deltaTime;
            }
            else
            {

                // Normalize the direction
                direction.Normalize();

                // Move the projectile towards the player's location
                transform.position += direction * flyTowardsSpeed * Time.deltaTime;

                // Check if the projectile is within the home in distance of the player
                if (Vector3.Distance(transform.position, player.transform.position) <= homeInDistance)
                {
                    // Start homing in on the player
                    StartHomingIn();
                }
            }

        }


        // Get the screen position of the object
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // Check if the object is outside the camera view
        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
        {
            // Destroy the object
            Destroy(gameObject);
        }
    }

    

    // The function to start homing in on the player
    void StartHomingIn()
    {
        isHomingIn = true;
    }

    // The function to handle collision with the player
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the projectile has collided with the player
        if (collider.gameObject.tag == "Player")
        {
            // Delete the projectile
            Debug.Log("hit");
            collider.gameObject.SendMessage("OnHit");
            StartCoroutine(explode());
        } else if (collider.gameObject.tag == "Player Attack")
        {
            StartCoroutine(explode());
        }
    }

    private IEnumerator explode()
    {
        /*
        anim.SetTrigger("hit");
        // Get the current runtime of the animation
        float runtime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        // Wait for the animation to finish
        while (runtime > 0.0f)
        {
            runtime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            yield return null;
        }
        */
        
        // Destroy the projectile
        Destroy(gameObject);
        yield return null;
    }
}



