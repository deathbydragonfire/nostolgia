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
    [SerializeField, Range(0f, 1f)] float jumpLandingVolumeMin;
    [SerializeField, Range(0f, 1f)] float jumpLandingVolumeMax;


    [Header("Attack")]
    [SerializeField] AudioSource attackSource;
    [SerializeField] AudioClip attackClip;
    [SerializeField, Range(0f, 1f)] float attackVolumeMin;
    [SerializeField, Range(0f, 1f)] float attackVolumeMax;

    [Header("Laser")]
    [SerializeField] AudioSource laserSource;
    [SerializeField] AudioClip laserClip;
    [SerializeField, Range(0f, 1f)] float laserVolume;

    [Header("Growl")]
    [SerializeField] AudioSource growlSource;
    [SerializeField] AudioClip growlClip;
    [SerializeField, Range(0f, 1f)] float growlVolume;

    [Header("Boss Hit")]
    [SerializeField] AudioSource bossHitSource;
    [SerializeField] AudioClip bossHitClip;
    [SerializeField, Range(0f, 1f)] float bossHitVolume;

    [Header("Player Hit")]
    [SerializeField] AudioSource playerHitSource;
    [SerializeField] AudioClip playerHitClip;
    [SerializeField, Range(0f, 1f)] float playerHitVolume;

    [Header("Projectile")]
    [SerializeField] AudioSource projectileSource;
    [SerializeField] AudioClip projectileClip;
    [SerializeField, Range(0f, 1f)] float projectileVolume;

    [Header("Projectile Explode")]
    [SerializeField] AudioSource projectileExplodeSource;
    [SerializeField] AudioClip projectileExplodeClip;
    [SerializeField, Range(0f, 1f)] float projectileExplodeVolume;
    public bool fadeoutCompleted = false;


    private void Update()
    {
        fadeoutCompleted = masterSoundController.fadeoutCompleted;
        if (Input.GetKeyDown(KeyCode.L))
            PlayBossHit();
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

    public void PlayProjectileExplode()
    {
        projectileExplodeSource.PlayOneShot(projectileExplodeClip, projectileExplodeVolume);
    }
}
