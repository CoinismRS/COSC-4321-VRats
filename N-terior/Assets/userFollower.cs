using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userFollower : MonoBehaviour
{

    [SerializeField] private Transform lookAt;
    [SerializeField] private float followSpeed;
    private Transform _thisTransform;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float distance = 10.0f;

    private bool isCentered = false;

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

            Vector3 targetPosition = FindTargetPosition();

            MoveTowards(targetPosition);

            if (ReachedPosition(targetPosition))
                isCentered = true;


        }

    }

    private Vector3 FindTargetPosition()
    {
        return cameraTransform.position + (cameraTransform.forward * distance);
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        transform.position += (targetPosition - transform.position) * 0.025f;
    }

    private bool ReachedPosition(Vector3 targetPosition)
    {
        
        return Vector3.Distance(targetPosition, transform.position) < 0.5f;
    }
}
