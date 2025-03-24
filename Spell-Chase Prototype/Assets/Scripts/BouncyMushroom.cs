using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyMushroom : MonoBehaviour
{
    public float jumpEffect = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit bouncy mushroom");
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpEffect, 0), ForceMode.Impulse);
    }
}
