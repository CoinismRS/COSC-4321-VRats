using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.Networking;
using System.IO;
using System.Net;
using UnityEditor;
using TMPro;


public class ColorLoader : MonoBehaviour
{
    private const string hostUrl = "http://20.84.56.123:8080/";
    private const string getColorUrl = hostUrl + "colors"; // Example GET endpoint
    public GameObject colorItemPrefab;
    public TextMeshProUGUI priceTextPrefab;

    public float colorPrice;
    public Color selectedColor;
    public double totalPrice;

    private changeWallColor surfaceArea;
    public GameObject textMesh;
    public GameObject textMesh2;

    public changeWallColor wallChanger;

    private OVRSceneManager oVRSceneManager;

    public Transform individualWallsContentPanel;
    public Transform allWallsContentPanel;

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

    void Start()
    {
       if(wallChanger == null)
       {
            Debug.LogError("WallChanger component not found. Please assign it in the inspector.");
            return; // Stop further execution if wallChanger is not assigned
        }
        LoadColors();
    }

    void LoadColors()
    {
        ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
        // Create a UnityWebRequest object to make the HTTP request
        UnityWebRequest www = UnityWebRequest.Get(getColorUrl);
        StartCoroutine(SendWebRequest(www));
    }

    // Loads the color data from the colors.json file.
    IEnumerator SendWebRequest(UnityWebRequest www)
    {
        // Send the request and wait for the response
        yield return www.SendWebRequest();

        // Check if there were any errors
        if (www.result != UnityWebRequest.Result.Success)
        {
            // Log the error if the request fails
            Debug.LogError("Failed to load color data: " + www.error);
        }
        else
        {
            // Extract JSON data from the response
            string dataAsJson = www.downloadHandler.text;
            OVRSceneRoom room = FindObjectOfType<OVRSceneRoom>();

            // Deserialize the JSON to the ColorList object
            ColorList colorList = JsonUtility.FromJson<ColorList>(dataAsJson);
            Debug.Log(colorList);

            PopulateUI(colorList, individualWallsContentPanel, allWallsContentPanel);
        }
    }


    // Converts a hex color string to a color object
    Color HexToColor(string hex)
    {
        // Remove the '#' from string
        hex = hex.Replace("#", "");

        // Parse the r, g, and b values from the hex string.
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        // Return color object
        return new Color32(r, g, b, 255);
    }


    public static double CustomCeiling(double value)
    {
        int integralPart = (int)value;
        if (value > integralPart)
        {
            return integralPart + 1;
        }
        else
        {
            return integralPart;
        }
    }



    // Populates UI with color items
    void PopulateUI(ColorList colorList, Transform individualWallsContentPanel, Transform allWallsContentPanel)
    {

        // Delete existing color items in the content panel
        foreach (Transform child in individualWallsContentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in allWallsContentPanel)
        {
            Destroy(child.gameObject);
        }

        // Iterate over each color in the JSON
        foreach (ColorInfo color in colorList.colors)
        {
            // Create new color item prefab as a child of the content panel
            GameObject individualWalls = Instantiate(colorItemPrefab, individualWallsContentPanel);
            GameObject allWalls = Instantiate(colorItemPrefab, allWallsContentPanel);

            // Set the color name in the prefab's Text component
            individualWalls.GetComponentInChildren<TextMeshProUGUI>().text = color.name;
            allWalls.GetComponentInChildren<TextMeshProUGUI>().text = color.name;

            // Assign a random price between 30 and 70 to the color
            color.price = (Mathf.Round(Random.Range(30f, 70f) * 2) / 2) - 0.01f; // 21 is exclusive
            TextMeshProUGUI individualWallPriceTextComponent = Instantiate(priceTextPrefab, individualWalls.transform).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI allWallsPriceTextComponent = Instantiate(priceTextPrefab, allWalls.transform).GetComponent<TextMeshProUGUI>();
            
            // display price value for price
            individualWallPriceTextComponent.text = "$" + color.price.ToString("F2") + " / gallon";
            allWallsPriceTextComponent.text = "$" + color.price.ToString("F2") + " / gallon";

            // Set its color based on the hex value.
            Image individualWallBackground = individualWalls.GetComponentInChildren<Image>();
            individualWallBackground.color = HexToColor(color.hex);
            Image allWallsBackground = allWalls.GetComponentInChildren<Image>();
            allWallsBackground.color = HexToColor(color.hex);

            individualWalls.GetComponent<Button>().onClick.AddListener(() => setColorAndShowColor(individualWallBackground.color, color.name));
            allWalls.GetComponent<Button>().onClick.AddListener(() => SelectColorFromCatalog(allWallsBackground.color, color.price, color.name));
        }
    }

    private void SelectColorFromCatalog(Color color, float price, string colorName)
    {
        selectedColor = color;
        colorPrice = price;
        wallChanger.ChangeAllWallColors(color);
        textMesh2.GetComponent<TextMeshProUGUI>().text = colorName;
        textMesh2.GetComponent<TextMeshProUGUI>().color = color;
    }


    public void setColorAndShowColor(Color color, string colorName)
    {
        selectedColor = color;
        textMesh.GetComponent<TextMeshProUGUI>().text = colorName;
        textMesh.GetComponent<TextMeshProUGUI>().color = color;
        //GameObject showColorItem = GameObject.FindGameObjectWithTag("SelectedColor");
        //Debug.Log("look here:" + showColorItem);
        //showColorItem.GetComponent<TextMeshPro>().text = "sdkjfnksdjfjkfds";
        //showColorItem.GetComponent<TextMeshPro>().color = color;
        //Debug.Log("here I am: " + showColorItem.GetComponent<TextMeshPro>().text);
    }
}

