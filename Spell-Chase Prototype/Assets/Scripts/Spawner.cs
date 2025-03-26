using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool spawning = false;

    private float timer;
    public float spawnTime = 2f;

    [SerializeField] private Transform spawnLocation;

    Selection currentSelection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning)
        {
            spawning = true;
            Create();
            
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                currentSelection.Spawn();
                spawning = false;
            }
        }
    }

    void Create()
    {
        int i = Random.Range(0, (int)Collection.Spawnings.Potion3);
        Collection.Spawnings randomObject = (Collection.Spawnings)i;

        Selection item = SpawnShop.instance.CreateSpawn(randomObject);

        Debug.Log(item);
        currentSelection = item;

        //set location/rotation
        ((MonoBehaviour)item).gameObject.transform.position = spawnLocation.position;
        ((MonoBehaviour)item).gameObject.transform.rotation = spawnLocation.rotation;

        timer = currentSelection.spawnTime;
    }
}
