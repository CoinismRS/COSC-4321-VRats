using UnityEngine;
using UnityEngine.UI;

public class WallButton : MonoBehaviour
{
    private Color color; // Color to apply to the wall
    private changeWallColor wallChanger; // Reference to the wall changer script
    private int wallIndex; // Index of the wall this button will change

    public void Setup(Color newColor, changeWallColor changer, int index)
    {
        color = newColor;
        wallChanger = changer;
        wallIndex = index;

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ApplyColor);
        }
    }

    private void ApplyColor()
    {
        if (wallChanger != null)
        {
            wallChanger.changeSingleWallColor(wallIndex, color);
        }
    }
}
