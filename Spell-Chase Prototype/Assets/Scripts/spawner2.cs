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

    //Spawn chance for pickup. 10% chance.
    [SerializeField] 
    float pickupSpawnChance = 0.1f;

    public static bool spawnObstacle = true;

    [SerializeField] int spawnerID; // Unique ID (0, 1, or 2)

    //prefab indices for current spawn cycle. 3 lanes 
    private static readonly int[] currentIndices = new int[3] { -1, -1, -1 };

    //location of bookshelf in array - prefabs[0]
    private const int BOOKSHELF_INDEX = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnObstacle = true;
        StartCoroutine(PrefabSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PrefabSpawn()
    {
        while(spawnObstacle == true)
        {
            float wantedX = transform.position.x + Random.Range(Min, Max);    //transform.position.x is the x position of the spawner. Ensures obstacles spawn within the spawning range at the x position of spawner
            Vector3 position = new Vector3(wantedX, transform.position.y, transform.position.z);    //included z position so that obstacles spawn at z position of spawners

            //choose prefab and lane location
            int prefabIndex = ChoosePrefab();

            GameObject game = Instantiate(prefabs[prefabIndex], position, Quaternion.identity );

            //if random value is smaller or equal to than 10%, spawn pickup. Random.value is the same as Random.Range(0.0f, 1.0f).
            if (Random.value < pickupSpawnChance && SectionTrigger.isBossBattle == false)
            {
                //pickup at position of spawner
                Vector3 pickupPosition = new Vector3(wantedX, transform.position.y - 1f, transform.position.z + 2f);

                //select random pickup and spawn in game
                GameObject pickup = Instantiate(Pickups[Random.Range(0, Pickups.Length)], pickupPosition, Quaternion.identity);

            }

            if (Death.deathStatus == true || SectionTrigger.isBossBattle == true)
            {
                spawnObstacle = false;

                yield return null;
            }
            


            yield return new WaitForSeconds( timeSpawn );
            
        }
    }

    private int ChoosePrefab()
    {
        //choose initial prefab
        int prefabIndex = Random.Range(0, prefabs.Length);
        
        currentIndices[spawnerID] = prefabIndex;

        //spawner2 (ID 1) checks if all chose bookshelf
        if (spawnerID == 1)
        {
            //check if all spawners chose bookshelf
            if (currentIndices[0] == BOOKSHELF_INDEX && currentIndices[1] == BOOKSHELF_INDEX && currentIndices[2] == BOOKSHELF_INDEX)
            {
                //randomise again until prefab is not a bookshelf
                do
                {
                    prefabIndex = Random.Range(0, prefabs.Length);
                } 
                while (prefabIndex == BOOKSHELF_INDEX);
                
                currentIndices[spawnerID] = prefabIndex;
            }
        }

        //clear indices after spawning
        StartCoroutine(ClearIndices());

        return prefabIndex;
    }

    private IEnumerator ClearIndices()
    {
        yield return new WaitForEndOfFrame();
        
        currentIndices[spawnerID] = -1;
    }
}
