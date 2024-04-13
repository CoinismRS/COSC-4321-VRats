using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
  public FlexibleColorPicker fcp;
  public TextMeshProUGUI paintName;
  public TextMeshProUGUI wallArea;
  public changeWallColor wallPainter;

  public void StartBtn()
  {
    SceneManager.LoadScene("MainScene");
  }

  public void ExitBtn()
  {
    Application.Quit();
  }

  void Update()
  {
    Color currentColor = fcp.color;

    string hexColor = ColorUtility.ToHtmlStringRGB(currentColor);
    paintName.text = "#" + hexColor;

    wallArea.text = "Area Size: " + wallPainter.paintAreaNeeded.ToString("F2") + " square units";

  }
}