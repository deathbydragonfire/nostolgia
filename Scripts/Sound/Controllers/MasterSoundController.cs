using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MasterSoundController : MonoBehaviour
{
    public bool fadeoutCompleted;

    [Header("Mixer")]
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider ambientSlider;
    [SerializeField] Slider sfxSlider;
    float masterVolume = 1f;
    float musicVolume = 1f;
    float sfxVolume = 1f;
    float ambientVolume = 1f;

    [Header("Controllers")]
    [SerializeField] SoundController musicController1;
    [SerializeField] SoundController musicController2;
    [SerializeField] SoundController ambientController1;
    [SerializeField] SoundController ambientController2;
    [SerializeField] SFXController sfxController;

    string currentScene;

    [Header("Music")]
    [SerializeField] AudioSO outsideMusic;
    [SerializeField] AudioSO bossMusicFirstLoop;
    [SerializeField] AudioSO bossMusicLoop;

    [Header("Ambient")]
    [SerializeField] AudioSO outsideAmbient;
    [SerializeField] AudioSO caveAmbient;
    [SerializeField] float outsideVolumeMax = 0f;
    [SerializeField] float outsideVolumeMin = -10f;
    [SerializeField] float caveVolumeMax = 0f;
    [SerializeField] float caveVolumeMin = -40f;
    [SerializeField] float yHigh = -8.65f;
    [SerializeField] float yLow = -80f;

    [Header("Player Hit")]
    [SerializeField, Range(20f, 22000f)] float cutoffLow = 2000f;
    [SerializeField, Range(20f, 22000f)] float cutoffHigh = 22000f;
    [SerializeField] float cutoffAttack = 0.5f;
    [SerializeField] float cutoffSustain = 1f;
    [SerializeField] float cutoffDecay = 0.5f;

    Coroutine cutCoroutine;

    GameObject player;

    private static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = gameObject;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        masterVolume = PlayerPrefs.GetFloat("masterVol", 1f);
        ambientVolume = PlayerPrefs.GetFloat("ambientVol", 1f);
        musicVolume = PlayerPrefs.GetFloat("musicVol", 1f);
        sfxVolume = PlayerPrefs.GetFloat("sfxVol", 1f);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        fadeoutCompleted = (musicController1.fadeOutCompleted || currentScene == "TitleScene") && ambientController1.fadeOutCompleted &&
                           musicController2.fadeOutCompleted && ambientController1.fadeOutCompleted;

        if (currentScene == "TitleScene")
        {
            mixer.SetFloat("ambient1Vol", ConvertVolumeToDB(ambientVolume));
        }
        if (currentScene == "OutsideScene")
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
                return;
            float playerY = player.transform.position.y;
            float outsideVolume = (outsideVolumeMax - outsideVolumeMin) / (yHigh - yLow) * (playerY - yLow) + outsideVolumeMin;
            outsideVolume = Mathf.Clamp(outsideVolume, outsideVolumeMin, outsideVolumeMax) + ConvertVolumeToDB(ambientVolume);
            mixer.SetFloat("ambient1Vol", outsideVolume);
            float caveVolume = (caveVolumeMin - caveVolumeMax) / (yHigh - yLow) * (playerY - yLow) + caveVolumeMax;
            caveVolume = Mathf.Clamp(caveVolume, caveVolumeMin, caveVolumeMax) + ConvertVolumeToDB(ambientVolume);
            mixer.SetFloat("ambient2Vol", caveVolume);
        }
        else if (currentScene == "BossScene2")
        {
            mixer.SetFloat("ambient1Vol", ConvertVolumeToDB(ambientVolume));
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TitleScene" && currentScene != "TitleScene")
        {
            if (currentScene != "OutsideScene")
                musicController1.PlaySoundLooped(outsideMusic);
        }
        else if (scene.name == "OutsideScene" && currentScene != "OutsideScene")
        { 
            if (currentScene != "TitleScene")
                musicController1.PlaySoundLooped(outsideMusic);
            ambientController1.PlaySoundLooped(outsideAmbient);
            ambientController2.PlaySoundLooped(caveAmbient);
        }
        else if (scene.name == "BossScene2" && currentScene != "BossScene2")
        {
            ambientController1.PlaySoundLooped(caveAmbient);
            PlayBossMusic();
        }
        currentScene = scene.name;
    }

    public void EndScene()
    {
        if (currentScene != "TitleScene")
            musicController1.StopSound();
        musicController2.StopSound();
        ambientController1.StopSound();
        ambientController2.StopSound();
    }

    public void PlayBossMusic()
    {
        double startTime = AudioSettings.dspTime + 0.1;
        double durationFirstLoop = (double) bossMusicFirstLoop.clip.samples / bossMusicFirstLoop.clip.frequency;
        musicController1.PlaySoundScheduledOnce(bossMusicFirstLoop, startTime);
        musicController2.PlaySoundScheduledLooped(bossMusicLoop, startTime + durationFirstLoop);
        ambientController1.StopSound();
    }

    public void StopBossMusic() 
    {
        musicController1.StopSound();
        musicController2.StopSound();
        ambientController1.PlaySoundLooped(caveAmbient);
    }

    public void Cut()
    {
        if (cutCoroutine == null)
            cutCoroutine = StartCoroutine(_Cut());
    }

    IEnumerator _Cut()
    {
        float timer = 0f;
        while (timer < cutoffAttack)
        {
            timer += Time.deltaTime;
            mixer.SetFloat("masterLPCut", Mathf.Lerp(cutoffHigh, cutoffLow, timer / cutoffAttack));
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(cutoffSustain);

        timer = 0f;
        while (timer < cutoffDecay)
        {
            timer += Time.deltaTime;
            mixer.SetFloat("masterLPCut", Mathf.Lerp(cutoffLow, cutoffHigh, timer / cutoffDecay));
            yield return new WaitForEndOfFrame();
        }

        cutCoroutine = null;
    }

    public void SetMasterVolume()
    {
        masterVolume = masterSlider.value;
        PlayerPrefs.SetFloat("masterVol", masterVolume);
        mixer.SetFloat("masterVol", ConvertVolumeToDB(masterVolume));
    }

    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVol", musicVolume);
        mixer.SetFloat("musicVol", ConvertVolumeToDB(musicVolume));
    }

    public void SetAmbientVolume()
    {
        ambientVolume = ambientSlider.value;
        PlayerPrefs.SetFloat("ambientVol", ambientVolume);
    }

    public void SetSFXVolume()
    {
        sfxVolume = sfxSlider.value;
        PlayerPrefs.SetFloat("sfxVol", sfxVolume);
        mixer.SetFloat("sfxVol", ConvertVolumeToDB(sfxVolume));
    }

    float ConvertVolumeToDB(float volume)
    {
        return Mathf.Log10(volume) * 20f;
    }

}
