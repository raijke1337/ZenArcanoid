using System;
using UnityEngine;

public enum ObjectType
{
    Unset,
    Solid,
    Point,
    Passthrough
}

public delegate void CollisionEventsHandler(Collision item, BouncyItemComponent comp);
public delegate void GameEventHandler(bool value);

public enum BuildType
{
    Editor,
    Build
}

[Serializable]
public struct SpawnerTask
{
    public int total;
    public int split;
    public float delay;
    public SpawnerTask(int _total, int _split, float _delay)
    {
        total = _total;
        split = _split;
        delay = _delay;
    }
}
