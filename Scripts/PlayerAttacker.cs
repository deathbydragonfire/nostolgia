using System.Collections;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    // The attack cooldown period, in seconds
    public float attackCooldown = 1.0f;

    // A timer to keep track of the attack cooldown
    private float attackTimer = 0.0f;
    bool attacking = false;
    public Animator anim;
    public OrbAttacker orb;


    void Update()
    {
        // Increment the attack timer
        attackTimer += Time.deltaTime;

        // Check if the attack button is pressed and the attack timer is greater than or equal to the attack cooldown
        if (Input.GetAxisRaw("Attack") > 0 && attackTimer >= attackCooldown)
        {
            Debug.Log("attack");
            // Reset the attack timer
            attackTimer = 0.0f;

            // Start the attack event
            anim.SetTrigger("attacking");
            orb.Attack();
        }
    }

    
}
