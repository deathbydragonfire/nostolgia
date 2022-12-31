using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField] MasterSoundController masterSoundController;

    [Header("Footsteps")]
    [SerializeField] AudioSource footstepSource;
    [SerializeField] List<AudioClip> footstepClips = new List<AudioClip>();
    [SerializeField, Range(0f, 1f)] float footstepVolumeMin = 0.8f;
    [SerializeField, Range(0f, 1f)] float footstepVolumeMax = 1f;

    [Header("Jump Landing")]
    [SerializeField] AudioSource jumpLandingSource;
    [SerializeField] AudioClip jumpLandingClip;
    [SerializeField, Range(0f, 1f)] float jumpLandingVolumeMin = 0.4f;
    [SerializeField, Range(0f, 1f)] float jumpLandingVolumeMax = 0.5f;


    [Header("Attack")]
    [SerializeField] AudioSource attackSource;
    [SerializeField] AudioClip attackClip;
    [SerializeField, Range(0f, 1f)] float attackVolumeMin = 0.4f;
    [SerializeField, Range(0f, 1f)] float attackVolumeMax = 0.5f;

    [Header("Laser")]
    [SerializeField] AudioSource laserSource;
    [SerializeField] AudioClip laserClip;
    [SerializeField, Range(0f, 1f)] float laserVolume = 0.4f;

    [Header("Growl")]
    [SerializeField] AudioSource growlSource;
    [SerializeField] AudioClip growlClip;
    [SerializeField, Range(0f, 1f)] float growlVolume = 0.4f;

    [Header("Boss Hit")]
    [SerializeField] AudioSource bossHitSource;
    [SerializeField] AudioClip bossHitClip;
    [SerializeField, Range(0f, 1f)] float bossHitVolume = 0.4f;

    [Header("Player Hit")]
    [SerializeField] AudioSource playerHitSource;
    [SerializeField] AudioClip playerHitClip;
    [SerializeField, Range(0f, 1f)] float playerHitVolume = 0.4f;

    [Header("Projectile")]
    [SerializeField] AudioSource projectileSource;
    [SerializeField] AudioClip projectileClip;
    [SerializeField, Range(0f, 1f)] float projectileVolume = 0.4f;

    public bool fadeoutCompleted = false;


    private void Update()
    {
        fadeoutCompleted = masterSoundController.fadeoutCompleted;
    }
    public void PlayFootstep()
    {
        int index = Random.Range(0, footstepClips.Count);
        float volume = Random.Range(footstepVolumeMin, footstepVolumeMax);
        footstepSource.PlayOneShot(footstepClips[index], volume);
    }

    public void PlayJumpLanding()
    {
        float volume = Random.Range(jumpLandingVolumeMin, jumpLandingVolumeMax);
        jumpLandingSource.PlayOneShot(jumpLandingClip, volume);
    }

    public void PlayAttack()
    {
        float volume = Random.Range(attackVolumeMin, attackVolumeMax);
        attackSource.PlayOneShot(attackClip, volume);
    }

    public void PlayLaser()
    {
        laserSource.PlayOneShot(laserClip, laserVolume);
    }

    public void PlayGrowl()
    {
        growlSource.PlayOneShot(growlClip, growlVolume);
    }

    public void PlayBossHit()
    {
        bossHitSource.PlayOneShot(bossHitClip, bossHitVolume);
    }

    public void PlayProjectile()
    {
        projectileSource.PlayOneShot(projectileClip, projectileVolume);
    }

    public void EndScene()
    {
        masterSoundController.EndScene();
    }

    public void PlayBossMusic()
    {
        masterSoundController.PlayBossMusic();
    }

    public void StopBossMusic()
    {
        masterSoundController.StopBossMusic();
    }

    public void PlayPlayerHit()
    {
        //masterSoundController.Cut();
        playerHitSource.PlayOneShot(playerHitClip, playerHitVolume);
    }
}
