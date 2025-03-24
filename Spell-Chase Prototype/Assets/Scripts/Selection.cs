using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Selection 
{
    float spawnTime { get; }

    void Spawn();
}
