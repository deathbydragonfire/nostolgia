using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] float startDelay = 0f;
    [SerializeField, Range(0f, 3f)] float fadeInTime = 0f;
    [SerializeField, Range(0f, 3f)] float fadeOutTime = 0f;

    Coroutine soundCoroutine = null;

    public bool fadeOutCompleted = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // TODO read volume parameter from settings
        PlaySound();
    }

    public void PlaySound()
    {
        if (soundCoroutine == null)
            soundCoroutine = StartCoroutine(_PlaySound());
    }

    IEnumerator _PlaySound()
    {
        yield return new WaitForSeconds(startDelay);

        float timer = 0f;
        // TODO read this parameter from settings
        float endVolume = 1f;
        audioSource.volume = 0f;
        audioSource.Play();
        while (timer <= fadeInTime) {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, endVolume, timer / fadeInTime);
            yield return new WaitForEndOfFrame();
        }
        soundCoroutine = null;
    }

    public void StopSound()
    {
        if (soundCoroutine != null)
        {
            StopCoroutine(soundCoroutine);
            soundCoroutine = null;
        }
        StartCoroutine(_StopSound());
    }

    IEnumerator _StopSound()
    {
        float timer = 0f;
        float startVolume = audioSource.volume;
        while (timer <= fadeOutTime)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeOutTime);
            yield return new WaitForEndOfFrame();
        }
        audioSource.Stop();
        fadeOutCompleted = true;
    }

    public void PauseSound()
    {
        audioSource.Pause();
    }

    public void ResumeSound()
    {
        audioSource.Play();
    }
}
