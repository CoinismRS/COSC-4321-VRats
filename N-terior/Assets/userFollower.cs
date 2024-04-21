using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userFollower : MonoBehaviour
{
    // Start is called before the first frame update
    /* [SerializeField] private Transform cameraTransform;
     [SerializeField] private float distance = 3.0f;

     private bool isCentered = false;

     // Built-in Unity *Magic*
     private void OnBecameInvisible()
     {
         isCentered = false;
     }

     private void Update()
     {
         if (!isCentered)
         {
             // Find the position we need to be at
             Vector3 targetPosition = FindTargetPosition();

             // Move just a little bit at a time
             MoveTowards(targetPosition);

             // If we've reached the position, don't do anymore work
             if (ReachedPosition(targetPosition))
                 isCentered = true;
         }
     }

     private Vector3 FindTargetPosition()
     {
         // Let's get a position infront of the player's camera
         return cameraTransform.position + (cameraTransform.forward * distance);
     }

     private void MoveTowards(Vector3 targetPosition)
     {
         // Instead of a tween, that would need to be constantly restarted
         transform.position += (targetPosition - transform.position) * 0.025f;
     }

     private bool ReachedPosition(Vector3 targetPosition)
     {
         // Simple distance check, can be replaced if you wish
         return Vector3.Distance(targetPosition, transform.position) < 0.1f;
     }*/

    [SerializeField] private Transform lookAt;
    [SerializeField] private Transform transformToFollow;
    [SerializeField] private float followSpeed;
    private Transform _thisTransform;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float distance = 10.0f;

    private bool isCentered = false;
    public MeshRenderer mr;

    // Built-in Unity *Magic*
    private void OnBecameInvisible()
    {
        isCentered = false;
    }


    private void Start()
    {
        _thisTransform = transform;
    }

    private void Update()
    {
        _thisTransform.LookAt(lookAt, Vector3.up);
        _thisTransform.Rotate(xAngle: 0f, yAngle: 180f, zAngle: 0f);
        var newPosition =  new Vector3(_thisTransform.position.x, _thisTransform.position.y, _thisTransform.position.z);
        var followPosition = new Vector3(_thisTransform.position.x, _thisTransform.position.y, _thisTransform.position.z);
        newPosition.x = Mathf.Lerp(a: newPosition.x, b: newPosition.x, t: followSpeed * Time.deltaTime);
        newPosition.y = Mathf.Lerp(a: newPosition.y, b: newPosition.y, t: followSpeed * Time.deltaTime);
        newPosition.z = Mathf.Lerp(a: newPosition.z, b: newPosition.z, t: followSpeed * Time.deltaTime);
        transform.position = newPosition;

        if (!isCentered)
        {
            Debug.Log("I have running");
            // Find the position we need to be at
            Vector3 targetPosition = FindTargetPosition();

            // Move just a little bit at a time
            MoveTowards(targetPosition);

            // If we've reached the position, don't do anymore work
            if (ReachedPosition(targetPosition))
                isCentered = true;
                Debug.Log("I have stopped");
        }
    }

    private Vector3 FindTargetPosition()
    {
        // Let's get a position infront of the player's camera
        return cameraTransform.position + (cameraTransform.forward * distance);
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        // Instead of a tween, that would need to be constantly restarted
        transform.position += (targetPosition - transform.position) * 0.025f;
    }

    private bool ReachedPosition(Vector3 targetPosition)
    {
        // Simple distance check, can be replaced if you wish
        return Vector3.Distance(targetPosition, transform.position) < 0.1f;
    }
}
