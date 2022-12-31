using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MasterSoundController : MonoBehaviour
{
    public bool fadeoutCompleted;

    [Header("Mixer")]
    [SerializeField] AudioMixer mixer;
    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public float ambientVolume = 1f;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            StopBossMusic();
        fadeoutCompleted = musicController1.fadeOutCompleted && ambientController1.fadeOutCompleted &&
                           musicController2.fadeOutCompleted && ambientController1.fadeOutCompleted;

        mixer.SetFloat("masterVol", ConvertVolumeToDB(masterVolume));
        mixer.SetFloat("musicVol", ConvertVolumeToDB(musicVolume));
        mixer.SetFloat("sfxVol", ConvertVolumeToDB(sfxVolume));

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            return;

        float playerY = player.transform.position.y;
        if (currentScene == "OutsideScene")
        {
            float outsideVolume = (outsideVolumeMax - outsideVolumeMin) / (yHigh - yLow) * (playerY - yLow) + outsideVolumeMin;
            outsideVolume = Mathf.Clamp(outsideVolume, outsideVolumeMin, outsideVolumeMax) + ConvertVolumeToDB(ambientVolume);
            mixer.SetFloat("ambient1Vol", outsideVolume);
            float caveVolume = (caveVolumeMin - caveVolumeMax) / (yHigh - yLow) * (playerY - yLow) + caveVolumeMax;
            caveVolume = Mathf.Clamp(caveVolume, caveVolumeMin, caveVolumeMax) + ConvertVolumeToDB(ambientVolume);
            mixer.SetFloat("ambient2Vol", caveVolume);
        }
        else if (currentScene == "BossScene")
        {
            mixer.SetFloat("ambient1Vol", ConvertVolumeToDB(ambientVolume));
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "OutsideScene" && currentScene != "OutsideScene")
        {
            musicController1.PlaySoundLooped(outsideMusic);
            ambientController1.PlaySoundLooped(outsideAmbient);
            ambientController2.PlaySoundLooped(caveAmbient);
        }
        else if (scene.name == "BossScene" && currentScene != "BossScene")
        {
            ambientController1.PlaySoundLooped(caveAmbient);
            PlayBossMusic();
        }

        currentScene = scene.name;
    }

    public void EndScene()
    {
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

    float ConvertVolumeToDB(float volume)
    {
        return Mathf.Log10(volume) * 20f;
    }
}
