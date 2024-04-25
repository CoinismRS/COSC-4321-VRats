using UnityEngine;
using TMPro;

public class CalculateCost : MonoBehaviour
{
    public TextMeshProUGUI totalPaintCostText;
    public TextMeshProUGUI paintNeeded;
    public TextMeshProUGUI totalFloorCostText;
    public TextMeshProUGUI totalFloorAreaText;
    public TextMeshProUGUI pricePerSqftText;
    public changeWallColor wallCalculations;
    public floorChanger floorCalculations;

    public ColorLoader colorLoader;
    

    public void CalculateTotalCost()
    {

        // Display the amount of cans of paint needed to paint the wall
        int cansNeeded = wallCalculations.cans;
        paintNeeded.text = cansNeeded.ToString() + " can(s)";


        float totalPaintCost = cansNeeded * colorLoader.colorPrice;
        totalPaintCostText.text = "$" + totalPaintCost.ToString("F2");
    }

    public void CalculateFloorCost()
    {
        double totalFloorCost = floorCalculations.totalFloorCost;
        totalFloorCostText.text = "$" + totalFloorCost.ToString("F2");

        double totalFloorArea = floorCalculations.floorArea;
        totalFloorAreaText.text = totalFloorArea.ToString("F2") + "sqft";

    }
}