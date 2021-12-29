﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnerComp : MonoBehaviour
{
    [SerializeField, Tooltip("Objects inside cubes")]
    List<GameObject> samples = new List<GameObject>();
    [SerializeField, Tooltip("Cube prefab")]
    BouncyItemComponent _cube;

    private Vector3 spaceCrystalScale = new Vector3(0.2f,0.2f,0.2f);
    private Vector3 spaceCrystalPos = new Vector3(0, -0.3f, 0);


    private void OnValidate()
    {
        if (samples.Count == 0)
        {
            Debug.LogError("Add samples to spawner component!");
        }
    }

    /// <summary>
    /// Spawn some items
    /// todo: add prefab selection to spawn
    /// todo: different objects
    /// </summary>
    /// <param name="total">int cubes spawned per execution</param>
    /// <param name="split">will spawn int onjects with delay</param>
    /// <param name="delay">float time delay for split mode</param>
    public List<BouncyItemComponent> SpawnCubes(SpawnerTask task)
    {
        var list = new List<BouncyItemComponent>();
        for (int num = 0; num < task.total; num ++)
        {            
            BouncyItemComponent cube = Instantiate(_cube, UnityEngine.Random.insideUnitSphere * 5,UnityEngine.Random.rotation,GameObject.Find("CubesEmpty").transform);
            list.Add(cube);
            cube.SetObjectType(ObjectType.Cube);
            // magic number todo fix

            GameObject crystal = samples[UnityEngine.Random.Range(0, samples.Count)];
            crystal = Instantiate(crystal,cube.transform);
            crystal.transform.localPosition += spaceCrystalPos;
            crystal.transform.localScale = spaceCrystalScale;
        }
        list.TrimExcess();
        return list;
        // unused todo
    }



    

}