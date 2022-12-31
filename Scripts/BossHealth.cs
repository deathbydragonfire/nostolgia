using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private int health = 25;
    private bool dead = false;
    private float timer = 0f;
    public float deathtime = 2.5f;
    public Animator anim;
    SFXController sfxController;
    MasterSoundController masterSoundController;
    // Start is called before the first frame update

    // The prefab for the heart UI element
    public GameObject heartPrefab;

    // The parent object for the heart UI elements
    public Transform heartParent;

    // The list of heart UI elements
    private List<GameObject> hearts = new List<GameObject>();

    public float heartspacing = 6f;
    void Start()
    {
        sfxController = FindObjectOfType<SFXController>();
        masterSoundController = FindObjectOfType<MasterSoundController>();

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
            gameObject.SendMessage("OnDeath");
            anim.SetTrigger("dead");
            dead = true;
            masterSoundController.StopBossMusic();
            sfxController.PlayBossDeath();
        } else if (dead)
        {
            timer += Time.deltaTime;
            if (timer > deathtime)
            {
                Destroy(gameObject);
            }
        }

        UpdateHealthUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Attack")
        {
            sfxController.PlayBossHit();
            Debug.Log("Boss Health: " + health);
            health--;
        }
    }

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
