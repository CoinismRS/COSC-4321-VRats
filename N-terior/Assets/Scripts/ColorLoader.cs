using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System.Net;
using UnityEditor;

public class ColorLoader : MonoBehaviour
{
    private const string localhostUrl = "http://20.84.56.123:8080/"; // Replace 3000 with your server's port
    private const string getColorUrl = localhostUrl + "colors"; // Example GET endpoint
    public GameObject colorItemPrefab;
    public Text priceTextPrefab;
    public Transform contentPanel;

    void Start()
    {
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
            
            // Deserialize the JSON to the ColorList object
            ColorList colorList = JsonUtility.FromJson<ColorList>(dataAsJson);
            Debug.Log(colorList);
            
            // Populate UI with the deserialized color data
            PopulateUI(colorList);
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

    // Populates UI with color items
    void PopulateUI(ColorList colorList)
    {
        
        // Delete existing color items in the content panel
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Iterate over each color in the JSON
        foreach (ColorInfo color in colorList.colors)
        {
            // Create new color item prefab as a child of the content panel
            GameObject newItem = Instantiate(colorItemPrefab, contentPanel);
            
            // Set the color name in the prefab's Text component
            newItem.GetComponentInChildren<Text>().text = color.name;
            
            // Assign a random price between 15 and 20 to the color
            color.price = (Mathf.Round(Random.Range(15f, 20.5f) * 2) / 2) - 0.01f; // 21 is exclusive
            Text priceTextComponent = Instantiate(priceTextPrefab, newItem.transform).GetComponent<Text>();
            // display price value for price
            priceTextComponent.text = "$" + color.price.ToString("F2") + " / gallon";
            
            // Set its color based on the hex value.
            Image background = newItem.GetComponentInChildren<Image>();
            background.color = HexToColor(color.hex);
        }
    }
}
