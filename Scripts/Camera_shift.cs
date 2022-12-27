using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_shift : MonoBehaviour
{
    public CameraFollow cam;
    public Vector2 new_min_bounds;
    public Vector2 new_max_bounds;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.minBounds = new_min_bounds;
        cam.maxBounds = new_max_bounds;
    }
}
