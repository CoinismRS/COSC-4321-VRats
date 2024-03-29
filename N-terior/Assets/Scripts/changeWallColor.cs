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
    private OVRScenePlane floor;
    private PassthroughProjectionSurfaceBuildingBlock passThroughWindow;
    //public OVRScenePrefabOverride wallPrefab;
    //public OVRScenePrefabOverride windows;

    public Material wallMat;
    public Material floorMat;
    public Color wallColor = Color.clear;
    public Color floorColor = Color.clear;

    private void Awake()
    {
        Debug.Log("Awakeeeeee");
        oVRSceneManager = FindObjectOfType<OVRSceneManager>();
        oVRSceneManager.SceneModelLoadedSuccessfully += SceneLoaded;
        /*oVRSceneManager.PrefabOverrides.Add(wallPrefab);
        oVRSceneManager.PrefabOverrides.Add(windows);
        oVRSceneManager.PrefabOverrides.Add(art);*/
        //Debug.Log("Overrides" + oVRSceneManager.PrefabOverrides.Count);
        //oVRSceneManager.PrefabOverrides[0] = wallPrefab;


    }


    private void Update()
    {
        if (room != null)
        {
            if (wallColor != Color.clear && floorColor != Color.clear)
            {


                wallMat.color = wallColor;
                floorMat.color = floorColor;
                var ceilingMaterial = ceiling.GetComponent<MeshRenderer>();
                var floorMaterial = floor.GetComponent<MeshRenderer>();
                ceilingMaterial.enabled = true;
                floorMaterial.enabled = true;
                ceilingMaterial.material = wallMat;
                floorMaterial.material = floorMat;
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
        floor = room.Floor;
        //passThroughWindow = FindObjectOfType<PassthroughProjectionSurfaceBuildingBlock>();
        //OVRScenePlane window = passThroughWindow.GetComponent<OVRScenePlane>();
        //var mesh = passThroughWindow.GetComponentInChildren<Transform>();
        //passThroughWindow.transform.localScale = new Vector3(1, 1, 1);
        //Debug.Log("Height:" + window.Height);
        //Debug.Log("Width:" + window.Width);
        //mesh.localScale = new Vector3(window.Height, window.Height, 1);
        //OVRSceneManager.Classification.WallFace = "Wall";
        //art = FindObjectOfType<OVRSceneManager.Classification>();

    }
}
