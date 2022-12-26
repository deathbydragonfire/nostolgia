using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The target that the camera should follow
    public Transform target;

    // The boundaries of the camera movement
    public Vector2 minBounds;
    public Vector2 maxBounds;

    void Update()
    {
        // Get the current position of the camera
        Vector3 currentPos = transform.position;

        // Get the position of the target
        Vector3 targetPos = target.position;

        // Calculate the new position of the camera based on the target's position
        Vector3 newPos = new Vector3(targetPos.x, targetPos.y, currentPos.z);

        // Ensure that the new position of the camera is within the defined boundaries
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        // Set the position of the camera to the new position
        transform.position = newPos;
    }
}
