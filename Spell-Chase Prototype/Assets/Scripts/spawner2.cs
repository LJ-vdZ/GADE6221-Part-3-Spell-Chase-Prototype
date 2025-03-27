using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner2 : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;

    [SerializeField] float timeSpawn = 1f;

    [SerializeField] float Min;

    [SerializeField] float Max;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrefabSpawn() );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PrefabSpawn()
    {
        while( true)
        {
            //var wanted = Random.Range(Min, Max);
            //var position = new Vector3( wanted, transform.position.y);

            float wantedX = transform.position.x + Random.Range( Min, Max );    //transform.position.x is the x position of the spawner. Ensures obstacles spawn within the spawning range at the x position of spawner
            Vector3 position = new Vector3(wantedX, transform.position.y, transform.position.z);    //included z position so that obstacles spawn at z position of spawners

            GameObject game = Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.identity );
            yield return new WaitForSeconds( timeSpawn );
            //Destroy(gameObject, 5f);     Solved the mystery to the disappearing spawners XD
        }
    }
}
