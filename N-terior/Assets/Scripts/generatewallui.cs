using UnityEngine;
using UnityEngine.UI;

public class GenerateWallUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform contentPanel;

    void Start()
    {
        // Subscribe to the scene loaded event
        OVRSceneManager oVRSceneManager = FindObjectOfType<OVRSceneManager>();
        oVRSceneManager.SceneModelLoadedSuccessfully += SceneLoaded;
    }

    private void SceneLoaded()
    {
        // Get the loaded room and its walls
        OVRSceneRoom room = FindObjectOfType<OVRSceneRoom>();
        if (room != null)
        {
            CreateUIForWalls(room.Walls);
        }
    }

    private void CreateUIForWalls(OVRScenePlane[] walls)
    {
        foreach (OVRScenePlane wall in walls)
        {
            GameObject newButton = Instantiate(buttonPrefab, contentPanel);
            newButton.GetComponentInChildren<Text>().text = wall.name;
            newButton.GetComponent<Button>().onClick.AddListener(() => ChangeWallColor(wall));
        }
    }

    private void ChangeWallColor(OVRScenePlane wall)
    {
        Debug.Log("Change color of " + wall.name);
        // Logic to change the wall's color goes here
    }
}
