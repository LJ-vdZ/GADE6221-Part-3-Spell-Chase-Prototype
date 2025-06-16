using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : ObstaclePassedScore
{
    public GameObject boss;
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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BossSpawn());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (spawned == false && oneCoroutine == false && score >= 30 && score < 70)
        {
            oneCoroutine = true;
            /*spawnerOne.SetActive(false);
            spawnerTwo.SetActive(false);
            spawnerThree.SetActive(false);*/
            StartCoroutine(BossSpawn());

            //BossSpawned?.Invoke();
        } 

       if (spawned == true && score >= 100)
        {
            LeaveBoss();

            //BossDespawned?.Invoke();
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
        BossDespawned?.Invoke();

        //levelsCompleted++;
        //LevelIncreased?.Invoke(levelsCompleted);  // Notify listeners of new level
    }

    IEnumerator BossSpawn()
    {

        if (spawned == false && score >= 30 && score < 100)
        {
            yield return new WaitForSeconds(12f);
            BringBoss();
        }
    }
}
