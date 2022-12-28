using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MasterSoundController : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    [SerializeField] SoundController musicController;
    [SerializeField] SoundController ambientController1;
    [SerializeField] SoundController ambientController2;
    [SerializeField] SFXController sfxController;

    string currentScene;
    public bool fadeoutCompleted;

    [Header("Music")]
    [SerializeField] AudioSO outsideMusic;

    [Header("Ambient")]
    [SerializeField] AudioSO outsideAmbient;
    [SerializeField] AudioSO caveAmbient;
    [SerializeField] float outsideVolumeMax = 0f;
    [SerializeField] float outsideVolumeMin = -10f;
    [SerializeField] float caveVolumeMax = 0f;
    [SerializeField] float caveVolumeMin = -40f;
    [SerializeField] float yHigh = -8.65f;
    [SerializeField] float yLow = -80f;

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
        
        fadeoutCompleted = musicController.fadeOutCompleted && ambientController1.fadeOutCompleted;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        float playerY = player.transform.position.y;
        if (currentScene == "OutsideScene")
        {
            float outsideVolume = (outsideVolumeMax - outsideVolumeMin) / (yHigh - yLow) * (playerY - yLow) + outsideVolumeMin;
            outsideVolume = Mathf.Clamp(outsideVolume, outsideVolumeMin, outsideVolumeMax);
            mixer.SetFloat("ambient1Vol", outsideVolume);
            float caveVolume = (caveVolumeMin - caveVolumeMax) / (yHigh - yLow) * (playerY - yLow) + caveVolumeMax;
            caveVolume = Mathf.Clamp(caveVolume, caveVolumeMin, caveVolumeMax);
            mixer.SetFloat("ambient2Vol", caveVolume);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "OutsideScene" && currentScene != "OutsideScene")
        {
            musicController.PlaySound(outsideMusic);
            ambientController1.PlaySound(outsideAmbient);
            ambientController2.PlaySound(caveAmbient);
        }

        currentScene = scene.name;
    }

    public void EndScene()
    {
        musicController.StopSound();
        ambientController1.StopSound();
        ambientController2.StopSound();
    }
}
