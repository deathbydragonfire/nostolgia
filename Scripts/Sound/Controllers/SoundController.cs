using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    AudioSource audioSource;

    Coroutine soundCoroutine = null;

    AudioSO currentAudioSO;
    public bool fadeOutCompleted = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // TODO read volume parameter from settings
    }

    public void PlaySound(AudioSO audioSO)
    {
        currentAudioSO = audioSO;
        if (soundCoroutine == null)
            soundCoroutine = StartCoroutine(_PlaySound());
    }

    IEnumerator _PlaySound()
    {
        yield return new WaitForSeconds(currentAudioSO.delayTime);

        float timer = 0f;
        // TODO read this parameter from settings
        float endVolume = currentAudioSO.volume * 1f;
        audioSource.volume = 0f;
        audioSource.clip = currentAudioSO.clip;
        audioSource.Play();
        while (timer <= currentAudioSO.fadeInTime) {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, endVolume, timer / currentAudioSO.fadeInTime);
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
        while (timer <= currentAudioSO.fadeOutTime)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / currentAudioSO.fadeOutTime);
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
