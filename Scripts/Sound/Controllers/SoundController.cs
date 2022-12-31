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

    public void PlaySoundLooped(AudioSO audioSO)
    {
        fadeOutCompleted = false;
        audioSource.loop = true;
        currentAudioSO = audioSO;
        if (soundCoroutine == null)
            soundCoroutine = StartCoroutine(_PlaySound());
    }

    public void PlaySoundOnce(AudioSO audioSO)
    {
        fadeOutCompleted = false;
        audioSource.loop = false;
        currentAudioSO = audioSO;
        if (soundCoroutine == null)
            soundCoroutine = StartCoroutine(_PlaySound());
    }

    public void PlaySoundScheduledLooped(AudioSO audioSO, double scheduledTime)
    {
        currentAudioSO = audioSO;
        audioSource.loop = true;
        audioSource.clip = audioSO.clip;
        audioSource.volume = audioSO.volume; // TODO SCALE THIS
        audioSource.PlayScheduled(scheduledTime);
    }

    public void PlaySoundScheduledOnce(AudioSO audioSO, double scheduledTime)
    {
        currentAudioSO = audioSO;
        audioSource.loop = false;
        audioSource.clip = audioSO.clip;
        audioSource.volume = audioSO.volume; // TODO SCALE THIS
        audioSource.PlayScheduled(scheduledTime);
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
        if (audioSource.isPlaying)
        {
            float timer = 0f;
            float startVolume = audioSource.volume;
            while (timer <= currentAudioSO.fadeOutTime)
            {
                timer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / currentAudioSO.fadeOutTime);
                yield return new WaitForEndOfFrame();
            }
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

    public bool isPlaying()
    {
        return audioSource.isPlaying;
    }
}
