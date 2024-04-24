using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using Meta.XR.BuildingBlocks;
using UnityEngine.UIElements;
using System.ComponentModel;

public class changeWallColor : MonoBehaviour
{
    private OVRSceneManager oVRSceneManager;
    private OVRSceneRoom room;
    public OVRScenePlane[] walls;
    public OVRScenePlane ceiling;

    public Material wallMat;
    public Material ceilingMat;
    public int wallIndex;
    public FlexibleColorPicker fcp;
    private List<double> wallStuffsArea = new List<double>();
    private GameObject roomModel;
    public double paintAreaNeeded;
    public double paintCostPerGallon = 75.0;
    public double totalPaintCost;
    public int cans;
    public bool reset = false;

    private void Awake()
    {
        oVRSceneManager = FindObjectOfType<OVRSceneManager>();
        oVRSceneManager.SceneModelLoadedSuccessfully += SceneLoaded;
        fcp.color = Color.clear;
    }

    private void Update()
    {
        if (paintAreaNeeded == 0)
        {
            var totalArea = getAreaOfRoom();
            var areaDeductions = getWallStuffsArea();
            paintAreaNeeded = totalArea - areaDeductions;
            var gallons = Math.Ceiling(paintAreaNeeded / 350.0);
            cans = (int)gallons;
            totalPaintCost = gallons * paintCostPerGallon;

        }

        if (room != null)
        {
            //var ceilingMaterial = ceiling.GetComponent<MeshRenderer>();
            if (fcp.color != Color.clear)
            {
                wallMat.color = fcp.color;
                
                /*if (wallIndex >= walls.Length)
                {
                    ceilingMat.color = fcp.color;
                }
                else
                {
                    wallMat.color = fcp.color;
                }*/

                //Commented code is still needed for later use probably don't delete
                //ceilingMaterial.enabled = true;
                //ceilingMaterial.material = wallMat;

                foreach (var wall in walls)
                {
                    var wallMaterial = wall.GetComponent<MeshRenderer>();
                    wallMaterial.enabled = true;
                    wallMaterial.material = wallMat;
                }

                //changeSingleWallColor(wallIndex, fcp.color);
                fcp.color = Color.clear;


            }

            /*if (reset)
            {
                foreach (var wall in walls)
                {
                    var wallMaterial = wall.GetComponent<MeshRenderer>();
                    wallMaterial.enabled = false;
                    
                }

            }*/
        }
    }

    public void SceneLoaded()
    {
        room = FindObjectOfType<OVRSceneRoom>();
        roomModel = room.gameObject;
        walls = room.Walls;
        ceiling = room.Ceiling;

        OVRSemanticClassification[] allClasses = roomModel.GetComponentsInChildren<OVRSemanticClassification>();
        foreach (var wallThing in allClasses)
        {
            if (wallThing.Contains("DOOR_FRAME") || wallThing.Contains("WINDOW_FRAME"))
            {
                var wallStuff = wallThing.gameObject;
                var plane = wallStuff.GetComponent<OVRScenePlane>();
                wallStuffsArea.Add((plane.Width * plane.Height));
            }

        }
    }

    public double getAreaOfRoom()
    {
        double area = 0.0;

        if (ceiling != null)
        {
            foreach (var wall in walls)
            {
                if (wall != null)
                {
                    area += (wall.Width * wall.Height);
                }
            }
        }
        return area * 10.76;
    }

    public double getWallStuffsArea()
    {
        double area = 0;

        foreach (var wallStuffArea in wallStuffsArea)
        {
            area += wallStuffArea;
        }
        return area * 10.76;
    }

    public void changeSingleWallColor(int wallIndex, Color color)
    {
        if (wallIndex >= walls.Length)
        {
            var ceilingMaterial = ceiling.GetComponent<MeshRenderer>();
            ceilingMaterial.enabled = true;
            ceilingMaterial.material = ceilingMat;
            ceilingMaterial.material.color = color;//ceilingMat.color;
            return;
        }


        var wall = walls[wallIndex];
        var wallMaterial = wall.GetComponent<MeshRenderer>();
        wallMaterial.enabled = true;
        wallMaterial.material = wallMat;
        wallMaterial.material.color = color;//wallMat.color;
    }

    public void ResetWallColor(int wallIndex)
    {
        if (wallIndex < 0 || wallIndex >= walls.Length)
        {
            // Out of range, handle error or ignore
            return;
        }

        // Change the color of the wall to the default color or material
        var wallMaterial = walls[wallIndex].GetComponent<MeshRenderer>();
        if (wallMaterial != null)
        {
            wallMaterial.enabled = false; // Assuming 'enabled' means visible in this context
            //wallMaterial.material = defaultWallMat; // Use the default material
            //wallMaterial.material.color = defaultColor; // Or just change the color
        }
    }

    public void resetAction()
    {
        foreach (var wall in walls)
        {
            var wallMaterial = wall.GetComponent<MeshRenderer>();
            wallMaterial.enabled = false;

        }
    }

    


}
