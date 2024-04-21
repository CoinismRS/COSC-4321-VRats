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
    private OVRScenePlane[] walls;
    private OVRScenePlane ceiling;
    
    public Material wallMat;
    public Material floorMat;
    
    // public FlexibleColorPicker fcp;
    private List<double> wallStuffsArea = new List<double>();
    private GameObject roomModel;
    public double paintAreaNeeded;

    private void Awake()
    {
        oVRSceneManager = FindObjectOfType<OVRSceneManager>();
        if (oVRSceneManager != null)
        {
            oVRSceneManager.SceneModelLoadedSuccessfully += SceneLoaded;
        }
        else
        {
            Debug.LogError("OVRSceneManager not found in the scene.");
        }
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        // Subscribe to the OnColorSelected event of the ColorLoader
        ColorLoader colorLoader = FindObjectOfType<ColorLoader>();
        if(colorLoader != null)
        {
            colorLoader.OnColorSelected += ChangeWallMaterialColor;
        }
        else
        {
            Debug.LogError("ColorLoader not found in the scene!");
        }
    }
    
    private void ChangeWallMaterialColor(Color newColor)
    {
        // Change the wall material to the new color
        wallMat.color = newColor;
        // Apply the updated color to the walls
        UpdateWallColors();
    }

    
    private void UpdateWallColors()
    {
        if (room != null)
        {
            var ceilingMaterial = ceiling.GetComponent<MeshRenderer>();
            ceilingMaterial.enabled = true;
            ceilingMaterial.material = wallMat;

            foreach (var wall in walls)
            {
                var wallMaterial = wall.GetComponent<MeshRenderer>();
                wallMaterial.enabled = true;
                wallMaterial.material = wallMat;
            }
        }
    }
    
    // private void Update()
    // {
    //     if (paintAreaNeeded == 0)
    //     {
    //         var totalArea = getAreaOfRoom();
    //         var areaDeductions = getWallStuffsArea();
    //         paintAreaNeeded = totalArea - areaDeductions;
    //   
    //     }
    //
    //     if (room != null)
    //     {
    //         var ceilingMaterial = ceiling.GetComponent<MeshRenderer>();
    //
    //         if (fcp.color != Color.clear)
    //         {
    //             wallMat.color = fcp.color;
    //
    //             ceilingMaterial.enabled = true;
    //             ceilingMaterial.material = wallMat;
    //             
    //             foreach (var wall in walls)
    //             {
    //                 var wallMaterial = wall.GetComponent<MeshRenderer>();
    //                 wallMaterial.enabled = true;
    //                 wallMaterial.material = wallMat;
    //             }
    //         } 
    //     } 
    // }

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
            area += (ceiling.Width * ceiling.Height);
            foreach (var wall in walls)
            {
                if (wall != null)
                {
                    area += (wall.Width * wall.Height);
                }
            }
        }
        return area;  
    }

    public double getWallStuffsArea()
    {
        double area = 0;

        foreach (var wallStuffArea in wallStuffsArea)
        {
            area += wallStuffArea; 
        }
        return area;
    }
}
