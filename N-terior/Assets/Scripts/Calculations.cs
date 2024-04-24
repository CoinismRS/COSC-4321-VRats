using UnityEngine;
using TMPro;

public class CalculateCost : MonoBehaviour
{
    public TextMeshProUGUI totalPaintCostText;
    public TextMeshProUGUI paintNeeded;
    public changeWallColor wallCalculations;
    

    public void CalculateTotalCost()
    {

        // Display the amount of cans of paint needed to paint the wall
        int cansNeeded = wallCalculations.cans;
        paintNeeded.text = cansNeeded.ToString() + " can(s)";


        double totalPaintCost = cansNeeded * 75;
        totalPaintCostText.text = "$" + totalPaintCost.ToString();
    }
}
