using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private int health = 5;
    private bool dead = false;
    float timer = 0f;
    public float deathtime = 1f;
    public string respawnScene;

    SFXController sfxController;

    // The prefab for the heart UI element
    public GameObject heartPrefab;

    // The parent object for the heart UI elements
    public Transform heartParent;

    // The list of heart UI elements
    private List<GameObject> hearts = new List<GameObject>();

    public float heartspacing = 6f;


    void Start()
    {
        sfxController = GameObject.FindObjectOfType<SFXController>();
        
        // Create the heart UI elements
        for (int i = 0; i < health; i++)
        {
            // Create a new heart UI element
            GameObject heart = Instantiate(heartPrefab, heartParent);
            heart.transform.position = new Vector3(heart.transform.position.x + heartspacing * i, heart.transform.position.y, heart.transform.position.z);
            

            // Add the heart to the list
            hearts.Add(heart);
        }
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
        } else
        {
            
        }
        
        UpdateHealthUI();
    }

    private void OnHit()
    {
        sfxController.PlayPlayerHit();
        health--;
        Debug.Log("health = " + health);
    }

    // The function to update the health UI
    private void UpdateHealthUI()
    {
        // Loop through the heart UI elements
        for (int i = 0; i < hearts.Count; i++)
        {
            // If the current health is less than or equal to the index of the current heart, disable the heart
            if (health <= i)
            {
                hearts[i].SetActive(false);
            }
            // Otherwise, enable the heart
            else
            {
                hearts[i].SetActive(true);
            }
        }
    }



}