using UnityEngine;

public class SimplePrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject previewPrefab;
    private GameObject currentPreview;

    private void Start() => currentPreview = Instantiate(previewPrefab);

    private void Update()
    {
        Ray ray = new Ray(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch),
                          OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            currentPreview.transform.position = hit.point;
            currentPreview.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Instantiate(prefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }
        }
    }
}
