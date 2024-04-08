using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using Meta.XR.BuildingBlocks;
using UnityEngine.UIElements;

public class changeWallColor : MonoBehaviour
{
    private OVRSceneManager oVRSceneManager;
    private OVRSceneRoom room;
    private OVRScenePlane[] walls;
    private OVRScenePlane ceiling;
    
    public Material wallMat;
    public Material floorMat;
    
    public FlexibleColorPicker fcp;
    private GameObject[] wallStuffs;
    private GameObject roomModel;
    private void Awake()
    {
        oVRSceneManager = FindObjectOfType<OVRSceneManager>();
        oVRSceneManager.SceneModelLoadedSuccessfully += SceneLoaded;
        //var r = oVRSceneManager.GetComponentInChildren<GameObject.FindGameObjectsWithTag("")>();
        //wallStuffs = GameObject.FindGameObjectsWithTag("wallStuff");
       
        fcp.color = Color.clear;
        /*foreach (var wallStuff in wallStuffs)
        {
            Debug.Log("hello");
            var stuff = wallStuff.GetComponent<OVRScenePlane>();
            //Debug.Log("prefabOverride:" + stuff.Width + stuff.Height);
        }*/

    }


    private void Update()
    {
        if (room != null)
        {
            wallStuffs = GameObject.FindGameObjectsWithTag("wallStuff");
            Debug.Log("wallStuffs:" + wallStuffs);
            var wallCeilingArea = getAreaOfRoom();
            //Debug.Log("Total" + wallCeilingArea);
            var ceilingMaterial = ceiling.GetComponent<MeshRenderer>();

            if (fcp.color != Color.clear)
            {
                wallMat.color = fcp.color;
              
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
        
    }

    public void SceneLoaded()
    {
        room = FindObjectOfType<OVRSceneRoom>();
        walls = room.Walls;
        ceiling = room.Ceiling;
    }

    public double getAreaOfRoom()
    {
        double area = (ceiling.Width * ceiling.Height);
        
        foreach (var wall in walls)
        {
            area += (wall.Width * wall.Height);
        }

        return area;  
    }
}
