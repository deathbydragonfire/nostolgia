using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private int health = 2;
    private bool dead = false;
    float timer = 0f;
    public float deathtime = 1f;
    public string respawnScene;
    void Start()
    {

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
        health--;
        Debug.Log("health = " + health);
    }

   

}