using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{

    public CharacterController2D cc2D;
    float h = 0f;
    bool j = false;
    public float runspeed = 40f;
    public Animator animator;
    Rigidbody2D rb2d;
    bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal") * runspeed;
        j = Input.GetAxisRaw("Jump") > 0;
        grounded = cc2D.IsGrounded();
        animator.SetFloat("speed", Mathf.Abs(h));
        animator.SetBool("jumping", j);
        animator.SetFloat("vertical", rb2d.velocity.y);
        animator.SetBool("grounded", grounded);
    }

    private void FixedUpdate()
    {

        cc2D.Move(h * Time.fixedDeltaTime, false, j);

    }

    void OnDeath()
    {
        GetComponent<CharacterController2D>().enabled = false;
        GetComponent<Player_movement>().enabled = false;
    }
}
