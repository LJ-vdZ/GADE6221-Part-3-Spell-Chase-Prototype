using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShop : MonoBehaviour
{
    //instance
    public static SpawnShop instance;

    public GameObject[] objects;
    private void Awake()
    {
        //singleton code
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    public Selection CreateSpawn(Collection.Spawnings objectToSpawn)
    {
        var go = Instantiate(objects[(int)objectToSpawn]);
        //Debug.Log(go.name + "Help");
        return go.GetComponent<Selection>();

        //return null;
    }
}
