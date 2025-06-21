using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject boss;

    public GameObject bossTwo;

    public GameObject spawnerOne;
    public GameObject spawnerTwo;
    public GameObject spawnerThree;
    public GameObject spawnerFour;
    public GameObject spawnerFive;
    public GameObject spawnerSix;
    private bool spawned = false;
    private bool oneCoroutine = false;
    public int levelsCompleted = 1;

    [SerializeField] float Min;

    [SerializeField] float Max;

    public static event System.Action BossSpawned;
    public static event System.Action BossDespawned;
    public static event System.Action LevelIncreased;

    private bool bossTwoSpawned = false;

    private bool bossTwoCoroutineStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BossSpawn());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //spawn boss 1
        if (spawned == false && oneCoroutine == false && ObstaclePassedScore.score >= 30 && ObstaclePassedScore.score < 100)
        {
             oneCoroutine = true;
             /*spawnerOne.SetActive(false);
             spawnerTwo.SetActive(false);
             spawnerThree.SetActive(false);*/
             StartCoroutine(BossSpawn());

             //BossSpawned?.Invoke();
        } 

        //despawn boss 1
        if (spawned == true && ObstaclePassedScore.score >= 100)
        {
             LeaveBoss();
 
             //BossDespawned?.Invoke();
             LevelIncreased?.Invoke();
        }

        //spawn boss 2 
        if (!bossTwoSpawned && !bossTwoCoroutineStarted && ObstaclePassedScore.score >= 80 && ObstaclePassedScore.score < 300)
        {
            bossTwoCoroutineStarted = true;
            StartCoroutine(BossTwoSpawn());
        }

        //despawn boss 2
        if (bossTwoSpawned && ObstaclePassedScore.score >= 300)
        {
            LeaveBossTwo();
            LevelIncreased?.Invoke();
        }
    }

    void BringBoss()
    {
        /*spawnerOne.SetActive(false);
        spawnerTwo.SetActive(false);
        spawnerThree.SetActive(false);
        spawnerFour.SetActive(true);
        spawnerFive.SetActive(true);
        spawnerSix.SetActive(true);
        float wantedX = transform.position.x + Random.Range(Min, Max);    //transform.position.x is the x position of the spawner. Ensures obstacles spawn within the spawning range at the x position of spawner
        Vector3 position = new Vector3(wantedX, transform.position.y, transform.position.z);    //included z position so that obstacles spawn at z position of spawners
        Quaternion rotation = Quaternion.Euler(0, 180, 0);  // Rotates boss to face front
        Instantiate(boss, position, rotation);*/

        spawned = true;
        //oneCoroutine = false;
        BossSpawned?.Invoke();  // Notify listeners that boss spawned
    }

    void LeaveBoss()
    {
        /*spawnerOne.SetActive(true);
        spawnerTwo.SetActive(true);
        spawnerThree.SetActive(true);
        spawnerFour.SetActive(false);
        spawnerFive.SetActive(false);
        spawnerSix.SetActive(false);

        if (boss != null)
        {
            Destroy(boss);
            boss = null;
        }*/
        spawned = false;
        oneCoroutine = false;
        BossDespawned?.Invoke();

        //levelsCompleted++;
        //LevelIncreased?.Invoke(levelsCompleted);  // Notify listeners of new level
    }

    void BringBossTwo()
    {
        Debug.Log("BringBossTwo() called");

        if (bossTwo == null)
        {
            Debug.LogError("BossTwo is not assigned in the Inspector");

            return;
        }

        //location of spawn point
        Vector3 position = new Vector3(0, 1, 5);
        Debug.Log($"Spawning bossTwo at position: {position}");


        Quaternion rotation = Quaternion.identity;

        GameObject spawnedBoss = Instantiate(bossTwo, position, rotation);

        //check boss two is instantiated
        

        if (spawnedBoss != null)
        {
            Debug.Log($"BossTwo instantiated successfully. Active: {spawnedBoss.activeSelf}, Layer: {LayerMask.LayerToName(spawnedBoss.layer)}, Scale: {spawnedBoss.transform.localScale}");
            MeshRenderer mr = spawnedBoss.GetComponentInChildren<MeshRenderer>();
            MeshFilter mf = spawnedBoss.GetComponentInChildren<MeshFilter>();
            if (mr != null && mf != null)
            {
                Debug.Log($"MeshRenderer enabled: {mr.enabled}, Mesh assigned: {mf.sharedMesh != null}, Material assigned: {mr.sharedMaterial != null}");
                if (mr.sharedMaterial != null)
                {
                    Debug.Log($"Material shader: {mr.sharedMaterial.shader.name}, Color: {mr.sharedMaterial.color}");
                }
            }
            else
            {
                Debug.LogWarning($"BossTwo missing components! MeshRenderer: {mr != null}, MeshFilter: {mf != null}");
            }
        }
        else
        {
            Debug.LogError("Failed to instantiate BossTwo");
        }

        // TestCube (keep for reference, but bossTwo should work after fixing)
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 1, 5);
        cube.name = "TestCube";
        Debug.Log($"TestCube spawned at position: {cube.transform.position}");
        


        bossTwoSpawned = true;
       
        BossSpawned?.Invoke();
    }

    void LeaveBossTwo()
    {
        bossTwoSpawned = false;

        bossTwoCoroutineStarted = false;

        BossDespawned?.Invoke();
    }

    IEnumerator BossSpawn()
    {

        if (spawned == false && ObstaclePassedScore.score >= 30 && ObstaclePassedScore.score < 100)
        {
            yield return new WaitForSeconds(1f);
            BringBoss();
        }
    }

    IEnumerator BossTwoSpawn()
    {
        Debug.Log("BossTwoSpawn coroutine started");

        
        //yield return new WaitForSeconds(2f);
        BringBossTwo();

        yield break;
        
    }
}
