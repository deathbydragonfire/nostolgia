using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    [SerializeField] GameObject settingsScreen;

    public void Start()
    {
        settingsScreen.SetActive(false);
    }

    public void Play()
    {
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
