using Firebase.Database;
using Google.MiniJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField]
    public TMP_InputField Name;
    private string playerID;

    public TMP_Text NameText;

    private DatabaseReference dbRef;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the root reference location of the database
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        
        //playerID = SystemInfo.deviceUniqueIdentifier;   //creates one ID per device, so only one player is stored. We need it to create an new ID for every game played
        //playerID = dbRef.Child("players").Push().Key;   //this create new unique ID in the firebase
    }

    //void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    public void CreatePlayer() 
    {
        //playerID = dbRef.Child("players").Push().Key;   //this create new unique ID in the firebase
        StartCoroutine(CheckPlayerExists());
    }

    private IEnumerator CheckPlayerExists() 
    {
        playerID = dbRef.Child("players").Push().Key;   //this create new unique ID in the firebase

        PlayerPrefs.SetString("playerID", playerID);    //save player id locally

        var playerData = dbRef.Child("players").Child(playerID).GetValueAsync();

        yield return new WaitUntil(predicate: () => playerData.IsCompleted);

        if (playerData.Result == null || !playerData.Result.Exists)
        {
            yield return StartCoroutine(GetNextGlobalGameNumber((int nextGameNumber) =>
            {
                Player newPlayer = new Player(Name.text);

                string json = JsonUtility.ToJson(newPlayer);
                dbRef.Child("players").Child(playerID).SetRawJsonValueAsync(json);

                dbRef.Child("players").Child(playerID).Child("score").SetValueAsync(0);

                dbRef.Child("players").Child(playerID).Child("gameNumber").SetValueAsync(nextGameNumber);
            }));

            //Player newPlayer = new Player(Name.text);

            //string json = JsonUtility.ToJson(newPlayer);

            //dbRef.Child("players").Child(playerID).SetRawJsonValueAsync(json);

            ////initialize score
            //dbRef.Child("players").Child(playerID).Child("score").SetValueAsync(0);

            ////initialize game number
            //dbRef.Child("players").Child(playerID).Child("gameNumber").SetValueAsync(0);
        }
        
    }

    //save score and game number when game ends as separate entries
    public void SaveGameData(int score, int gameNumber)
    {
        string playerID = PlayerPrefs.GetString("playerID");  //get the stored player ID

        dbRef.Child("players").Child(playerID).Child("score").SetValueAsync(score);

        dbRef.Child("players").Child(playerID).Child("gameNumber").SetValueAsync(gameNumber);

    }


    public IEnumerator GetNextGlobalGameNumber(Action<int> onCallback)
    {
        var playerData = dbRef.Child("players").GetValueAsync();

        yield return new WaitUntil(() => playerData.IsCompleted);

        int highestGameNumber = 0;

        if (playerData.Result != null)
        {
            DataSnapshot snapshot = playerData.Result;

            foreach (var player in snapshot.Children)
            {
                if (player.HasChild("gameNumber"))
                {
                    int gameNum = Convert.ToInt32(player.Child("gameNumber").Value);
                    if (gameNum > highestGameNumber)
                    {
                        highestGameNumber = gameNum;
                    }
                }
            }
        }

        onCallback.Invoke(highestGameNumber + 1); //next game number
    }
}
//https://youtu.be/59RBOBbeJaA
