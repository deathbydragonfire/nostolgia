using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    [SerializeField] GameObject settingsScreen;
    [SerializeField] Transition transition;

    public void Start()
    {
        settingsScreen.SetActive(false);
        transition.Fade(Transition.FadeDirection.In);
    }

    public void Play()
    {
        StartCoroutine(_Play());
    }

    IEnumerator _Play()
    {
        transition.Fade(Transition.FadeDirection.Out);
        MasterSoundController masterSoundController = GameObject.FindObjectOfType<MasterSoundController>();
        masterSoundController.EndScene();
        yield return new WaitUntil(() => masterSoundController.fadeoutCompleted && !transition.active);
        SceneManager.LoadScene("OutsideScene");
    }

    public void ShowSettings()
    {
        settingsScreen.SetActive(true);
    }

    public void HideSettings()
    {
        settingsScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
