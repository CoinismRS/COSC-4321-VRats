using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLoader : MonoBehaviour
{
    public GameObject colorItemPrefab;
    public Transform contentPanel;

    void Start()
    {
        LoadColors();
    }

    void LoadColors()
    {
        string filePath = System.IO.Path.Combine(Application.dataPath, "colors.json");
        if (System.IO.File.Exists(filePath))
        {
            string dataAsJson = System.IO.File.ReadAllText(filePath);
            ColorList colorList = JsonUtility.FromJson<ColorList>("{\"colors\":" + dataAsJson + "}");
            PopulateUI(colorList);
        }
        else
        {
            Debug.LogError("Cannot load color data!");
        }
    }

    void PopulateUI(ColorList colorList)
    {
        foreach (ColorInfo color in colorList.colors)
        {
            GameObject newItem = Instantiate(colorItemPrefab, contentPanel);
            newItem.GetComponentInChildren<Text>().text = color.name;

        }
    }
}
