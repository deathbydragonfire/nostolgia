using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField] AudioSource footstepSource;
    [SerializeField] List<AudioClip> footstepClips = new List<AudioClip>();
    [SerializeField, Range(0f, 1f)] float footstepVolumeMin = 0.8f;
    [SerializeField, Range(0f, 1f)] float footstepVolumeMax = 1f;

    [SerializeField] AudioSource jumpLandingSource;
    [SerializeField] AudioClip jumpLandingClip;
    [SerializeField, Range(0f, 1f)] float jumpLandingVolumeMin = 0.4f;
    [SerializeField, Range(0f, 1f)] float jumpLandingVolumeMax = 0.5f;


    public void PlayFootstep()
    {
        // TODO get SFX volume from settings and scale accordingly
        int index = Random.Range(0, footstepClips.Count);
        float volume = Random.Range(footstepVolumeMin, footstepVolumeMax); // SCALE THIS
        footstepSource.PlayOneShot(footstepClips[index], volume);
    }

    public void PlayJumpLanding()
    {
        float volume = Random.Range(jumpLandingVolumeMin, jumpLandingVolumeMax);
        jumpLandingSource.PlayOneShot(jumpLandingClip, volume);
    }
}