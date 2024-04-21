using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera camera;
    public float distance = 3.0f;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        // Calculate the target position in front of the camera
        Vector3 targetPosition = camera.transform.TransformPoint(new Vector3(0, 0, distance));

        // Smoothly move the object towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Calculate the position to look at (same x and z coordinates as the camera, but same y coordinate as the object)
        Vector3 lookAtPos = new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z);

        // Make the object look at the calculated position
        transform.LookAt(lookAtPos);
    }
}
