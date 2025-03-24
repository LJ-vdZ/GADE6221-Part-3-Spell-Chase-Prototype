using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBook : MonoBehaviour
{

    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //transform.position += new Vector3(speed, 0, 0);

        //get forward
        Vector3 forward = transform.forward;

        //never ever use float first
        Vector3 direction = forward * speed;

        //take our direction / speed and actually move our object
        transform.position += direction * Time.deltaTime;
    }
}
