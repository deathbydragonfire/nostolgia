using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update

    SFXController sfxController;

    private void Start()
    {
        checkSFXController();
    }

    public void step()
    {
        sfxController.PlayFootstep();
    }

    public void Land()
    {
        sfxController.PlayJumpLanding();
    }

    public void Laser()
    {
        sfxController.PlayLaser();
    }

    public void Hit()
    {
        sfxController.PlayPlayerHit();
    }

    private void checkSFXController()
    {
        if (sfxController == null)
            sfxController = FindObjectOfType<SFXController>();
    }
}
