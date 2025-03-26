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
            var wanted = Random.Range(Min, Max);
            var position = new Vector3( wanted, transform.position.y);
            GameObject game = Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.identity );
            yield return new WaitForSeconds( timeSpawn );
            //Destroy(gameObject, 5f);     Laz solved the mystery to the disappearing spawners XD
        }
    }
}
