using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner2 : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;

    [SerializeField] float timeSpawn = 1f;

    [SerializeField] float Min;

    [SerializeField] float Max;

    //Pickup GameObject
    [SerializeField] 
    GameObject[] Pickups;

    //Spawn chance for pickup. 20% chance.
    [SerializeField] 
    float pickupSpawnChance = 0.2f; 


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
            float wantedX = transform.position.x + Random.Range( Min, Max );    //transform.position.x is the x position of the spawner. Ensures obstacles spawn within the spawning range at the x position of spawner
            Vector3 position = new Vector3(wantedX, transform.position.y, transform.position.z);    //included z position so that obstacles spawn at z position of spawners

            GameObject game = Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.identity );

            //if random value is smaller or equal to than 20%, spawn pickup. Random.value is the same as Random.Range(0.0f, 1.0f).
            if (Random.value < pickupSpawnChance)
            {
                //pickup at position of spawner
                Vector3 pickupPosition = new Vector3(wantedX, transform.position.y - 1f, transform.position.z + 2f);

                //select random pickup and spawn in game
                GameObject pickup = Instantiate(Pickups[Random.Range(0, Pickups.Length)], pickupPosition, Quaternion.identity);

            }


            yield return new WaitForSeconds( timeSpawn );
            
        }
    }
}
