using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using Meta.XR.BuildingBlocks;

public class changeWallColor : MonoBehaviour
{
    private OVRSceneManager oVRSceneManager;
    private OVRSceneRoom room;
    private OVRScenePlane[] walls;
    private OVRScenePlane ceiling;
    
    public Material wallMat;
    public Material floorMat;
    
    public FlexibleColorPicker fcp;
    private void Awake()
    {
        oVRSceneManager = FindObjectOfType<OVRSceneManager>();
        oVRSceneManager.SceneModelLoadedSuccessfully += SceneLoaded;
        fcp.color = Color.clear;

    }


    private void Update()
    {
        if (room != null)
        {
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
}
