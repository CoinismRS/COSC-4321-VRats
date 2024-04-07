using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGenerator : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Get the position of the GameObject (the start position of the raycast)
        Vector3 raycastOrigin = transform.position;

        // Get the direction of the raycast (this can be adjusted based on your requirements)
        Vector3 raycastDirection = transform.forward;

        // Set the starting point of the LineRenderer to match the GameObject's position
        lineRenderer.SetPosition(0, raycastOrigin);

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit))
        {
            // If the raycast hits something, update the endpoint of the LineRenderer to the hit point
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // If the raycast doesn't hit anything, set a default endpoint (e.g., extend the ray to a maximum distance)
            lineRenderer.SetPosition(1, raycastOrigin + raycastDirection * 100f); // Change 100f to your desired maximum distance
        }
    }

    /*void ToggleLaser()
    {
        // TODO: Allow the user to toggle laser on and off via click/pokeable
        if (Input.GetMouseButtonDown(0))
        {
            lineRenderer.enabled = !lineRenderer.enabled;
        }
    }
    */
}