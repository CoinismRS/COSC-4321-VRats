using UnityEngine;

public class FloorOverlay : MonoBehaviour
{
    public GameObject roomModelPrefab; // Reference to your Room Model prefab
    private GameObject roomModelInstance; // Instance of the Room Model block

    void Start()
    {
        roomModelInstance = Instantiate(roomModelPrefab);
        CreateFloorOverlay();
    }

    void CreateFloorOverlay()
    {
        if (roomModelInstance == null)
        {
            Debug.LogError("Room Model is not instantiated.");
            return;
        }

        // Get the room's floor mesh
        Mesh roomFloorMesh = roomModelInstance.GetComponent<MetaRoomModel>().floorMesh;

        // Create a new GameObject for the floor overlay
        GameObject floorOverlay = new GameObject("FloorOverlay");
        floorOverlay.transform.position = Vector3.zero;
        floorOverlay.transform.rotation = Quaternion.identity;

        // Add a MeshFilter component and assign the room's floor mesh
        MeshFilter meshFilter = floorOverlay.AddComponent<MeshFilter>();
        meshFilter.mesh = roomFloorMesh;

        // Add a MeshRenderer component and assign a transparent material to it
        MeshRenderer meshRenderer = floorOverlay.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.color = new Color(1f, 1f, 1f, 0.5f); // Adjust alpha for transparency
    }
}
