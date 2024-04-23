using UnityEngine;
using UnityEngine.UI;

public class LevelBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent; // It's better to use Transform here
    public changeWallColor wallChanger; // Reference to the changeWallColor script
    private OVRSceneManager oVRSceneManager;
    private int selectedWallIndex = -1;


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
            /*if (selectedWallIndex != wallIndex)
            {
                Debug.Log("option 1");
                selectedWallIndex = wallIndex;
                wallChanger.changeSingleWallColor(wallIndex, Color.red); // Assume ChangeWallColor method exists
                return;
            }
            else
            {
                Debug.Log("option 2");
                wallChanger.ResetWallColor(wallIndex); // Assume ResetWallColor method exists
                selectedWallIndex = -1; // Reset selection
                return;
            }*/
            wallChanger.changeSingleWallColor(wallIndex, Color.red); // Use the index directly
        }
    }
}

/*using UnityEngine;
using UnityEngine.UI;

public class LevelBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public changeWallColor wallChanger;
    private int selectedWallIndex = -1; // -1 indicates no selection

    private void Awake()
    {
        // Find the OVRSceneManager and subscribe to the event
        var oVRSceneManager = FindObjectOfType<OVRSceneManager>();
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
        // Find the room and create buttons
        var room = FindObjectOfType<OVRSceneRoom>();
        if (room != null)
        {
            CreateButtons(room.Walls);
        }
        else
        {
            Debug.LogError("OVRSceneRoom not found in the scene.");
        }
    }

    private void CreateButtons(OVRScenePlane[] walls)
    {
        // Clear existing buttons
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        // Create a button for each wall
        for (int i = 0; i < walls.Length; i++)
        {
            var newButton = Instantiate(buttonPrefab, buttonParent);
            int wallIndex = i; // Wall index

            // Set button text
            var levelButton = newButton.GetComponent<LevelButton>();
            if (levelButton != null && levelButton.levelText != null)
            {
                levelButton.levelText.text = "Wall " + (wallIndex + 1);
            }

            // Add button click listener
            newButton.GetComponent<Button>().onClick.AddListener(() => SelectWall(wallIndex));
        }
    }

    private void SelectWall(int wallIndex)
    {
        // If selecting a new wall, change color; if selecting the same wall, reset color
        if (selectedWallIndex != wallIndex)
        {
            selectedWallIndex = wallIndex;
            wallChanger.changeSingleWallColor(wallIndex, Color.red); // Assume ChangeWallColor method exists
        }
        else
        {
            wallChanger.ResetWallColor(wallIndex); // Assume ResetWallColor method exists
            selectedWallIndex = -1; // Reset selection
        }

        Debug.Log(selectedWallIndex == -1 ? "Wall deselected" : "Selected wall: " + selectedWallIndex);
    }
}*/











