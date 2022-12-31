using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private int health = 100;
    private bool dead = false;
    float timer = 0f;
    public float deathtime = 1f;
    public string respawnScene;

    SFXController sfxController;

    void Start()
    {
        sfxController = GameObject.FindObjectOfType<SFXController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !dead)
        {
            Debug.Log("Dead");
            gameObject.SendMessage("OnDeath");
            GetComponent<Animator>().SetTrigger("dead");
            dead = true;
        } else if (dead)
        {
            timer += Time.deltaTime;
            if (timer > deathtime)
            {
                SceneManager.LoadScene(respawnScene);
            }
        }
    }

    private void OnHit()
    {
        sfxController.PlayPlayerHit();
        health--;
        Debug.Log("health = " + health);
    }

   

}