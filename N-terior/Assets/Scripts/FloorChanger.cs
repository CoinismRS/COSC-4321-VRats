using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using Meta.XR.BuildingBlocks;

public class floorChanger : MonoBehaviour
{
    private OVRSceneManager oVRSceneManager;
    private OVRSceneRoom room;
    private OVRScenePlane floor;
    public Material floorMat;
    public double floorArea;
    public Material shaggy;
    public Material tile;
    public Material wood;
    private double priceMultiplier;
    public double totalFloorCost;
    private void Awake()
    {
        oVRSceneManager = FindObjectOfType<OVRSceneManager>();
        oVRSceneManager.SceneModelLoadedSuccessfully += SceneLoaded;
    }

    public void ChangeMaterial(Material newMaterial)
    {
        floorMat = newMaterial;
    }

    private void Update()
    {
        if (room != null)
        {
            var floorMaterial = floor.GetComponent<MeshRenderer>();

            if (floorMat != null)
            {
                floorMaterial.enabled = true;
            }
            else
            {
                floorMaterial.enabled = false;
            }

            floorMaterial.material = floorMat;
            if (floorMat == shaggy)
            {
                priceMultiplier = 2.5;
            } 
            else if (floorMat == tile)
            {
                priceMultiplier = 10.0;
            } 
            else if (floorMat == wood)
            {
                priceMultiplier = 13.5;
            }

            totalFloorCost = floorArea * priceMultiplier;
            //floorMat = null;
        }
    }

    public void SceneLoaded()
    {
        room = FindObjectOfType<OVRSceneRoom>();
        floor = room.Floor;
        floorArea = (floor.Height * floor.Width) * 10.76;  
    }


    public void resetAction()
    {
        floorMat = null;
    }
}

