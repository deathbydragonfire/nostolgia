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
        checkSFXController();
        sfxController.PlayFootstep();
    }

    public void Land()
    {
        checkSFXController();
        sfxController.PlayJumpLanding();
    }

    public void Laser()
    {
        checkSFXController();
        sfxController.PlayLaser();
    }

    public void Hit()
    {
        checkSFXController();
        sfxController.PlayPlayerHit();
    }

    private void checkSFXController()
    {
        if (sfxController == null)
            sfxController = FindObjectOfType<SFXController>();
    }
}
