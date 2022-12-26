using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{

    public CharacterController2D cc2D;
    float h = 0f;
    bool j = false;
    public float runspeed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal") * runspeed;
        j = Input.GetAxisRaw("Jump") > 0;
       
    }

    private void FixedUpdate()
    {
        cc2D.Move(h * Time.fixedDeltaTime, false, j);

    }
}
