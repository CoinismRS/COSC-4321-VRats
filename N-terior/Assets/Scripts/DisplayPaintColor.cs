using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPaintColor : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public TextMeshProUGUI hexText;

    void Update()
    {
        Color currentColor = fcp.color;

        string hexColor = ColorUtility.ToHtmlStringRGB(currentColor);

        hexText.text = "#" + hexColor;  
    }
}
