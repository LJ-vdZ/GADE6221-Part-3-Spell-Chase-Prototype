using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHallway : MonoBehaviour
{
    [SerializeField]
    public int hallwaySpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move platform bit by bit along the x-axis at set speed
        transform.position += new Vector3(hallwaySpeed, 0, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);    //destroy hallway and everything in it when it collides with invisible destroy wall
        }
    }
}
