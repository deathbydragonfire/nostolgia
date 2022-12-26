using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();
    [SerializeField, Range(0f, 1f)] float volumeMin = 0.8f;
    [SerializeField, Range(0f, 1f)] float volumeMax = 1f;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayFootstep()
    {
        // TODO get SFX volume from settings and scale accordingly
        int index = Random.Range(0, clips.Count);
        float volume = Random.Range(volumeMin, volumeMax); // SCALE THIS
        AudioSource.PlayClipAtPoint(clips[index], player.transform.position, volume);
    }
}
