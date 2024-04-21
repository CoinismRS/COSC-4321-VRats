using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI paintName;
    public TextMeshProUGUI wallArea;
    public Material wallMat; // Ensure this is assigned via the Inspector or elsewhere in your code.
    public changeWallColor wallPainter; // Ensure this is assigned via the Inspector or elsewhere in your code.
    
    private ColorList colorList; // Using ColorList from ColorInfo.cs

    void Start()
    {
        LoadColors();
    }

    public void StartBtn()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    void LoadColors()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/colors.json");
        
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            // colorList = JsonUtility.FromJson<ColorList>(dataAsJson);
            colorList = JsonUtility.FromJson<ColorList>("{\"colors\":" + dataAsJson + "}");

        }
        else
        {
            Debug.LogError("Failed to load color data.");
        }
    }

    public void UpdateSelectedColor(ColorInfo colorInfo)
    {
        paintName.text = colorInfo.name;
        wallArea.text = "Price: $" + colorInfo.price.ToString("F2") + " / gallon";
        
        // Update the wall color
        wallMat.color = HexToColor(colorInfo.hex);
        wallPainter.UpdateWallColors(); // Assuming UpdateWallColors() is a method in wallPainter that applies the color
    }

    private Color HexToColor(string hex)
    {
        // Your existing HexToColor method.
        hex = hex.Replace("#", "");
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    // If there is any other code you need in this file, it would go here.

    // Removed the Update method entirely since we're handling color updates through event subscription now.
}
