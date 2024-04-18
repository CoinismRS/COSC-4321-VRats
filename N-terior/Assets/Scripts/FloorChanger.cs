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

            floorMaterial.material = floorMat;
        }
    }

    public void SceneLoaded()
    {
        room = FindObjectOfType<OVRSceneRoom>();
        floor = room.Floor;
    }
}

