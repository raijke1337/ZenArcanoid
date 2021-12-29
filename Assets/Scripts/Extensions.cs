using System;
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

public enum ObjectType
{
    Cube,
    Wall,
    Bonus,
    Trap,
    Player
}

public delegate void CollisionEventsHandler(BouncyItemComponent item);
public delegate void GameEventHandler();


[Serializable]
public struct SpawnerTask
{
    public int total;
    public int split;
    public float delay;
    public SpawnerTask(int _total,int _split,float _delay)
    {
        total = _total;
        split = _split;
        delay = _delay;
    }
}
