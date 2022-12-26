using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // The name of the scene to load
    public string sceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider is the player
        if (other.gameObject.tag == "Player")
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }
}
