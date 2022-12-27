using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterSoundController : MonoBehaviour
{
    [SerializeField] SoundController musicController;
    [SerializeField] SoundController ambientController;
    [SerializeField] SFXController sfxController;

    string currentScene;
    public bool fadeoutCompleted;

    [Header("Music")]
    [SerializeField] AudioSO outsideMusic;

    [Header("Ambient")]
    [SerializeField] AudioSO outsideAmbient;

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
        fadeoutCompleted = musicController.fadeOutCompleted && ambientController.fadeOutCompleted;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "OutsideScene" && currentScene != "OutsideScene")
        {
            musicController.PlaySound(outsideMusic);
            ambientController.PlaySound(outsideAmbient);
        }

        currentScene = scene.name;
    }

    public void EndScene()
    {
        musicController.StopSound();
        ambientController.StopSound();
    }
}
