using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class changeWallColor : MonoBehaviour
{
    private OVRSceneManager oVRSceneManager;
    private OVRSceneRoom room;
    private OVRScenePlane[] walls;
    private OVRScenePlane ceiling;
    private OVRScenePlane floor;
    //public OVRScenePrefabOverride wallPrefab;
    //public OVRScenePrefabOverride windows;
    //public OVRScenePrefabOverride art;
    public Material wallMat;
    public Material floorMat;
    //public Color wallColor;

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
            var ceilingMaterial = ceiling.GetComponent<MeshRenderer>();
            var floorMaterial = floor.GetComponent<MeshRenderer>();
            ceilingMaterial.material = wallMat;
            floorMaterial.material = floorMat;
            foreach (var wall in walls)
            {
                var wallMaterial = wall.GetComponent<MeshRenderer>();
                wallMaterial.material = wallMat;
            }

        }
    }

    public void SceneLoaded()
    {
        room = FindObjectOfType<OVRSceneRoom>();
        walls = room.Walls;
        ceiling = room.Ceiling;
        floor = room.Floor;
    }
}
