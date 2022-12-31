using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationEvents : MonoBehaviour
{
    SFXController sfxController;

    private void Start()
    {
        sfxController = GameObject.FindObjectOfType<SFXController>();
    }

    public void Hit()
    {
        sfxController.PlayBossHit();
    }

    public void Growl()
    {
        sfxController.PlayGrowl();
    }

    public void WakeUp()
    {
        FindObjectOfType<MasterSoundController>().PlayBossMusic();
    }
}
