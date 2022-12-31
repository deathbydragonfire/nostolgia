using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenUI : MonoBehaviour
{
    [SerializeField] GameObject settingsScreen;

    public void Start()
    {
        settingsScreen.SetActive(false);
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
