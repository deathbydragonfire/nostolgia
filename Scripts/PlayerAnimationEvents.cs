using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update

    public SFXController sfxController;
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

    private void checkSFXController()
    {
        if (sfxController == null)
            sfxController = FindObjectOfType<SFXController>();
    }
}
