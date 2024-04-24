using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuController : MonoBehaviour
{
  public FlexibleColorPicker fcp;
  public TextMeshProUGUI paintName;
  public TextMeshProUGUI wallArea;
  public TextMeshProUGUI floorArea;
  public changeWallColor wallPainter;
  public floorChanger floorController;
  
  private List<ColorData> colorList;

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
      string filePath = Path.Combine(Application.dataPath, "Scripts/colors.json");
      
      if (System.IO.File.Exists(filePath))
      {
          string dataAsJson = System.IO.File.ReadAllText(filePath);
          
          ColorListWrapper colorListWrapper = JsonUtility.FromJson<ColorListWrapper>("{\"colors\":" + dataAsJson + "}");
          
          colorList = colorListWrapper.colors;
      }
      else
      {
          Debug.LogError("Failed to load color data.");
      }
  }

  void Update()
  {
    wallTexts();
    floorTexts();
  }

  void wallTexts()
  {
    // Display Area Size
    wallArea.text = wallPainter.paintAreaNeeded.ToString("F2") + " sq ft.";

    // Display Color/Paint Selected
    Color currentColor = fcp.color;

    string hexColor = ColorUtility.ToHtmlStringRGB(currentColor);
    // Display the Hex Color
    paintName.text = "#" + hexColor;

    // Should match hex value with the ones in colors.json to retreive and display the paint name.
    bool matchFound = false;
    foreach (var colorData in colorList)
    {
      if (colorData.hex.Equals(hexColor, StringComparison.OrdinalIgnoreCase))
      {
        paintName.text = colorData.name;
        matchFound = true;
        break;
      }
    }

    // If the hex value is not one included in colors.json, display that to the user.
    if (!matchFound)
    {
      paintName.text = "No matching color found";
    }
  }

  void floorTexts()
  {
    //Display Area Size
    floorArea.text = floorController.floorArea.ToString("F2") + " sq ft.";
  }  

  [System.Serializable]
  public class ColorData
  {
    public string name;
    public string hex;
  }

  [System.Serializable]
  public class ColorListWrapper
  {
    public List<ColorData> colors;
  }
}