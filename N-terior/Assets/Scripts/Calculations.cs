using UnityEngine;
using TMPro;

public class CalculateCost : MonoBehaviour
{
    public TextMeshProUGUI totalPaintCostText;
    public TextMeshProUGUI paintNeeded;
    public TextMeshProUGUI totalFloorCostText;
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
        colorLoader.totalPrice = totalPaintCost;
    }

    public void CalculateFloorCost()
    {
        double totalFloorCost = floorCalculations.totalFloorCost;
        totalFloorCostText.text = "$" + totalFloorCost.ToString("F2");
    }
}