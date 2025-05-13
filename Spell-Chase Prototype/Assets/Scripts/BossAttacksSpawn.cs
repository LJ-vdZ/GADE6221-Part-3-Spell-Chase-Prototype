using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class BossAttacksSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] items;

    [SerializeField] float timeSpawn = 1f;

    [SerializeField] float Min;

    [SerializeField] float Max;

    public int chance;


    // Start is called before the first frame update
    void Start()
    {
        chance = Random.Range(0, 2);  //creates 50% chance of beam spawning
        StartCoroutine(ItemSpawn());
    }

    // Update is called once per frame
    /*void Update()
    {
        chance = Random.Range(0, 2);  //creates 50% chance of beam spawning
        if (chance == 1)
        {
            ItemSpawn();
        }
    }*/

    IEnumerator ItemSpawn()
    {
        while (true)
        {
            int chance = Random.Range(0, 2);  // 50/50 chance

            if (chance == 1)
            {
               

                float wanted = transform.position.x + Random.Range(Min, Max);
                Vector3 position = new Vector3(wanted, transform.position.y, transform.position.z);

                Instantiate(items[Random.Range(0, items.Length)], position, Quaternion.identity);
            }
            yield return new WaitForSeconds(timeSpawn);
        }
    }
}
