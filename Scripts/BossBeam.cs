using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeam : MonoBehaviour
{
    float timer = 0f;
    public float hitTime = 1f;
    public float hitEnd = 1.8f;
    public float endTime = 2f;
    bool hit = false;
    Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
        GameObject.FindObjectOfType<SFXController>().PlayLaser();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > endTime)
        {
            Destroy(gameObject);
        } else if (timer > hitEnd)
        {
            collider.enabled = false;
        } 
        
        else if (timer > hitTime && !hit)
        {
            collider.enabled = true;
            hit = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider has hit a game object with the "Player" tag
        if (other.gameObject.tag == "Player")
        {
            // Disable the collider
            collider.enabled = false;

            // Send a message to the player
            other.gameObject.SendMessage("OnHit");
            Debug.Log("hit");
        }
    }
}
