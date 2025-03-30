using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookshelfSpawner : MonoBehaviour
{
    //public GameObject bookshelfPrefab;

    ////assign 3 spawn locations in the Inspector
    //public Transform[] spawnPosition;

    ////keep track of score to determine when to spawn bookshelf
    ////private static int score = 0;                             

    ////track currently spawned bookshelf
    //private GameObject currentBookshelf;


    ////variable needed to be able to spawn bookshelves at regular intervals
    //private float spawnInterval = 4f; //interval to start spawning in seconds
    //private float timeSinceLastSpawn = 0f; //time tracker for spawn interval
    //private int lastScore = 0; //track the score when spawning started
    //private bool stopSpawning = false; //flag stop spawning bookshelves

    ////start spawinign again when a certain condition is met
    //private bool isSpawningActive = false; //flag to check if spawning is active

    //// Start is called before the first frame update
    //void Start()
    //{
    //    // set initial score when spawning starts
    //    lastScore = ObstaclePassedScore.GetScore();

    //    //check if player has 10 points to start spawning at the start of the game
    //    if (lastScore >= 10)
    //    {
    //        //start spawning bookshelves if player has 10 points
    //        isSpawningActive = true;
    //    }
    //    else
    //    {
    //        //do not spawn bookshelves
    //        isSpawningActive = false;
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //check if the spawning is not stopped
    //    if (isSpawningActive = true && !stopSpawning)
    //    {
    //        //track the time passed since last spawn
    //        timeSinceLastSpawn += Time.deltaTime;

    //        //check if enough time passed to spawn another bookshelf
    //        if (timeSinceLastSpawn >= spawnInterval)
    //        {
    //            //apawn bookshelf at random location
    //            SpawnBookshelf();

    //            //set timer back to 0
    //            timeSinceLastSpawn = 0f;
    //        }

    //        //check if player gained another 10 points to stop spawning
    //        if (ObstaclePassedScore.GetScore() >= lastScore + 10)
    //        {
    //            //stop spawning
    //            stopSpawning = true;

    //            //update last score
    //            lastScore = ObstaclePassedScore.GetScore();

    //            //wait for 10 second before spawning again
    //            Invoke("RestartSpawning", 5f); //restart spawning after 10 seconds
    //        }
    //    }
    //}

    //void SpawnBookshelf()
    //{
    //    int randomIndex;
    //    Transform spawnLocation;

    //    //pick random spawn point and ensure there's enough distance between bookshelf for player to move
    //    randomIndex = Random.Range(0, spawnPosition.Length);
    //    spawnLocation = spawnPosition[randomIndex];

    //    //spawn bookshelf at selected location
    //    Instantiate(bookshelfPrefab, spawnLocation.position, Quaternion.identity);   //no rotating

    //    //update last score 
    //    lastScore = ObstaclePassedScore.GetScore();

    //}

    ////restart spawning after 10 seconds of stopping
    //void RestartSpawning()
    //{
    //    stopSpawning = false; // Reset stop flag to resume spawning
    //    lastScore = ObstaclePassedScore.GetScore(); // Reset last score to current score
    //}
}
//REFERENCES
//Unity Documentation, [s.a.]. MonoBehaviour.Invoke. [online] Available at: <https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.Invoke.html> [Accessed 22 March 2025].
