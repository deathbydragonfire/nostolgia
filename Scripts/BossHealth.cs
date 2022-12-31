using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private int health = 1;
    private bool dead = false;
    private float timer = 0f;
    public float deathtime = 2.5f;
    public Animator anim;
    SFXController sfxController;
    MasterSoundController masterSoundController;
    // Start is called before the first frame update
    void Start()
    {
        sfxController = FindObjectOfType<SFXController>();
        masterSoundController = FindObjectOfType<MasterSoundController>();
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
}
