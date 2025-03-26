using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandOnFloor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hallway")) //check if obstacle lands on hallway floor
        {
            //parent the obstacle with the hallway so that it lands on it and move with it
            transform.SetParent(collision.gameObject.transform.parent);

            Debug.Log("Hallway active? " + collision.gameObject.activeSelf);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
