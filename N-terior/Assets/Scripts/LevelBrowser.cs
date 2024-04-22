using Normal.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LevelBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent; // It's better to use Transform here
    public changeWallColor wallChanger; // Reference to the changeWallColor script
    private OVRSceneRoom Help;
    private OVRScenePlane [] FHelp;
    private OVRSceneManager oVRSceneManager;



    private void Helper()
    {
        Help = FindObjectOfType<OVRSceneRoom>();
        FHelp = Help.Walls;
    }


    private void OnEnable()
    {
        oVRSceneManager = FindObjectOfType<OVRSceneManager>();
        oVRSceneManager.SceneModelLoadedSuccessfully += Helper;

        // Clear existing buttons to avoid duplicates
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        // Make sure wallChanger is assigned
        if (wallChanger == null)
        {
            // Find the changeWallColor script in the scene
            wallChanger = FindObjectOfType<changeWallColor>();
        }

        // Assuming wallChanger is not null from here on
        for (int i = 0; i < FHelp.Length+1; i++)
        {
      
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            int levelNum = i + 1; // This is your wall index

            // Set the button text
            LevelButton levelButton = newButton.GetComponent<LevelButton>();
            if (levelButton != null && levelButton.levelText != null)
            {
                levelButton.levelText.text = "Wall " + levelNum;
            }

            // Add the onClick listener
            newButton.GetComponent<Button>().onClick.AddListener(() => SelectLevel(levelNum));
        }
    }

    private void SelectLevel(int levelNum)
    {
        // Log for debug purposes
        Debug.Log("Change color of wall: " + levelNum);

        // If wallChanger is set, change the wall color
        if (wallChanger != null)
        {
            wallChanger.changeSingleWallColor(levelNum - 1); // Subtract 1 because arrays are 0-indexed
        }
    }
}




//Using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class LevelBrowser : MonoBehaviour
//{
//    public GameObject buttonPrefab;
//    public GameObject buttonParent;

//    private void OnEnable()
//    {
//        for (int i = 0; i < 5; i++)
//        {
//            GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
//            int levelNum = i + 1;
//            newButton.GetComponent<LevelButton>().levelText.text = ("Wall " + (i + 1)).ToString();
//            // newButton.GetComponent<Button>().onClick.AddListener(() => SelectLevel(GameManager.Instance.currentWorld, levelNum));
//        }
//    }

//    private void SelectLevel(int world, int level)
//    {
//        Debug.Log("Loaded level" + world + " - " + level);
//    }
//}
