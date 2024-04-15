using UnityEngine;

public class SimplePrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject previewPrefab;
    private GameObject currentPreview;

    // Used to keep track of the original rotation of the prefab
    private Quaternion originalRotation;

    private void Start()
    {
        // Instantiate the preview prefab and store its original rotation
        currentPreview = Instantiate(previewPrefab);
        originalRotation = currentPreview.transform.rotation;
    }

    private void Update()
    {
        // Cast a ray from the controller's position forward.
        Ray ray = new Ray(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch),
                          OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Position the current preview object at the hit point.
            currentPreview.transform.position = hit.point;
            // Align the current preview object with the hit normal.
            currentPreview.transform.up = hit.normal;

            // If the right trigger is squeezed, update the rotation of the preview object to match the controller's rotation.
            if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
            {
                Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
                currentPreview.transform.rotation = controllerRotation * originalRotation;
            }

            // If the A button is pressed, instantiate the prefab at the hit point with the current rotation.
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Instantiate(prefab, hit.point, currentPreview.transform.rotation);
            }
        }
    }
}
