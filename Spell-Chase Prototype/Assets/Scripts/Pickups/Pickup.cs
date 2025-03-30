using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : ObstaclePassedScore   //inherit from ObstaclePassedScore
{
    

    // Start is called before the first frame update
    //change to new void Start(). Hiding/overriding base method. 
    new void Start()
    {
        //call Start method from ObstaclePassedScore script to update UI
        //base.Start();   //base refers to ObstaclePassedScore class
        UpdateScoreInUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //if player collides with pickup
        {
            //check which pickup tag player collided with
            if (this.gameObject.CompareTag("GreenPotion"))   //green pickup tag
            {
                ObstaclePassedScore.score = score + 10; //boost player score

                UpdateScoreInUI();

                Destroy(gameObject);    

            }
        }
        

    }
}
//REFERENCES
//Microfsoft Learn, 2023. new modifier (C# Reference). [online] Available at: <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier> [Accessed 21 March 2025].
//Geeks for Geeks, 2025. C# Method Overriding. [online] Available at: <https://www.geeksforgeeks.org/c-sharp-method-overriding/>[Accessed 21 March 2025].