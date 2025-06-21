using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAssets : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("BluePotion") || other.gameObject.CompareTag("GreenPotion") || other.gameObject.CompareTag("RedPotion")) 
        {
            Destroy(other.gameObject);     //destory anything that is not the player
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
