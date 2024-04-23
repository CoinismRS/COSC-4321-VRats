using UnityEngine;
using UnityEngine.UI;

public class LevelBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent; // It's better to use Transform here
    public changeWallColor wallChanger; // Reference to the changeWallColor script
    private OVRSceneManager oVRSceneManager;

    private void Update()
    {
        oVRSceneManager = FindObjectOfType<OVRSceneManager>();
        if (oVRSceneManager != null)
        {
            oVRSceneManager.SceneModelLoadedSuccessfully += Helper;
        }
        else
        {
            Debug.LogError("OVRSceneManager not found in the scene.");
        }
    }

    private void Helper()
    {
        OVRSceneRoom room = FindObjectOfType<OVRSceneRoom>();
        if (room != null)
        {
            CreateButtons(room.Walls);
        }
        else
        {
            Debug.LogError("OVRSceneRoom not found in the scene.");
        }
    }

    private void OnEnable()
    {
        // Ensure wallChanger is assigned
        if (wallChanger == null)
        {
            // Find the changeWallColor script in the scene
            wallChanger = FindObjectOfType<changeWallColor>();
            if (wallChanger == null)
            {
                Debug.LogError("changeWallColor script not found in the scene.");
                return;
            }
        }

        // Assuming Helper will be called when the scene is loaded
        // and buttons will be created in CreateButtons method
    }

    private void CreateButtons(OVRScenePlane[] walls)
    {
        // Clear existing buttons to avoid duplicates
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("Length" + walls.Length);

        // Create buttons based on the number of walls
        for (int i = 0; i < walls.Length; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            int wallIndex = i; // Array is 0-indexed

            // Set the button text
            LevelButton levelButton = newButton.GetComponent<LevelButton>();
            if (levelButton != null && levelButton.levelText != null)
            {
                levelButton.levelText.text = "Wall " + (wallIndex + 1);
            }

            // Setup color and wall button functionality
            Color colorToApply = Color.red; // This should be dynamically assigned, for example from a color picker
            WallButton wallButton = newButton.AddComponent<WallButton>();
            wallButton.Setup(colorToApply, wallChanger, wallIndex);
        }
    }

    private void SelectWall(int wallIndex)
    {
        // Log for debug purposes
        Debug.Log("Selected wall with index: " + wallIndex);

        // If wallChanger is set, instruct it to change the wall color
        if (wallChanger != null)
        {
            wallChanger.changeSingleWallColor(wallIndex, Color.red); // Use the index directly
        }
    }
}









