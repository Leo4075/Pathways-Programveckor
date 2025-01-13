using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public Vector3 offset = new Vector3(0f, 0f, -10f); // The offset to apply to the camera's position
    public float smoothTime = 0.15f; // The time it takes for the camera to smooth out its movement
    private Vector3 velocity = Vector3.zero; // The velocity used by SmoothDamp

    [SerializeField] private Transform target; // The target the camera follows

    // Update is called once per frame
    private void Update()
    {
        // Calculate the desired position by adding the offset to the target's position
        Vector3 targetPosition = target.position + offset;

        // Smoothly transition to the target position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
