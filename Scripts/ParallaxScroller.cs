using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    // The layers of the parallax effect, starting with the furthest layer
    public Transform[] layers;

    // The speed at which each layer should move
    public float[] layerSpeeds;

    // The camera that the parallax effect should follow
    public Camera targetCamera;

    void Update()
    {
        // Get the position of the camera
        Vector3 cameraPos = targetCamera.transform.position;

        // Loop through each layer of the parallax effect
        for (int i = 0; i < layers.Length; i++)
        {
            // Calculate the new position of the layer based on the camera's position and the layer's speed
            Vector3 newPos = layers[i].position;
            newPos.x = cameraPos.x * layerSpeeds[i];

            // Set the position of the layer to the new position
            layers[i].position = newPos;
        }
    }
}
