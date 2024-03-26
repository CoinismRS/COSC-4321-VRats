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
    public OVRScenePrefabOverride wallPrefab;
    public OVRScenePrefabOverride windows;
    public OVRScenePrefabOverride art;


    private void Awake()
    {
        Debug.Log("Awakeeeeee");
        oVRSceneManager = FindObjectOfType<OVRSceneManager>();
        oVRSceneManager.SceneModelLoadedSuccessfully += SceneLoaded;
        oVRSceneManager.PrefabOverrides.Add(wallPrefab);
        oVRSceneManager.PrefabOverrides.Add(windows);
        oVRSceneManager.PrefabOverrides.Add(art);
        //Debug.Log("Overrides" + oVRSceneManager.PrefabOverrides.Count);
        //oVRSceneManager.PrefabOverrides[0] = wallPrefab;


    }

    public void SceneLoaded()
    {
        Debug.Log("func started");
        room = FindObjectOfType<OVRSceneRoom>();
        walls = room.Walls;
      

    }
}
