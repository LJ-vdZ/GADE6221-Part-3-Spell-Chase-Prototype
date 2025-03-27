using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //change objects rotation so it is up-right
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
