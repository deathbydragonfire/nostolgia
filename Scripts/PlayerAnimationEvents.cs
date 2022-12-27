using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update

    public SFXController footsteps_Controller;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            step();
        }
    }

    public void step()
    {
        Debug.Log("step");
        footsteps_Controller.PlayFootstep();
    }
}
