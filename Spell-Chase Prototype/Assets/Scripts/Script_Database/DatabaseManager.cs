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
        playerID = SystemInfo.deviceUniqueIdentifier;

        //get the root reference location of the database
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CreatePlayer() 
    {
        CheckPlayerExists();
    }

    private IEnumerator CheckPlayerExists() 
    {
        var playerData = dbRef.Child("players").Child(playerID).GetValueAsync();

        yield return new WaitUntil(predicate: () => playerData.IsCompleted);

        if (playerData.Result == null || !playerData.Result.Exists)
        {

            Player newPlayer = new Player(Name.text);

            string json = JsonUtility.ToJson(newPlayer);

            dbRef.Child("players").Child(playerID).SetRawJsonValueAsync(json);

            //initialize score
            dbRef.Child("players").Child(playerID).Child("score").SetValueAsync(0);

            //initialize game number
            dbRef.Child("players").Child(playerID).Child("gameNumber").SetValueAsync(0);
        }
    }

    //save score and game number when game ends as separate entries
    public void SaveGameData(int score, int gameNumber)
    {
        //var gameData = new Dictionary<string, object>
        //{
        //    { "score", score },
        //    { "gameNumber", gameNumber },
        //    { "playerName", Name.text } //store name, score and game number with each game
            
        //};

        //string gameID = dbRef.Child("players").Child(playerID).Child("games").Push().Key;


        dbRef.Child("players").Child(playerID).Child("score").SetValueAsync(score).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to save score: " + task.Exception);
            }
            else
            {
                Debug.Log("Score saved successfully: " + score);
            }
        });

        dbRef.Child("players").Child(playerID).Child("gameNumber").SetValueAsync(gameNumber).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to save game number: " + task.Exception);
            }
            else
            {
                Debug.Log("Game number saved successfully: " + gameNumber);
            }
        });

    }

    //name
    public IEnumerator GetName(Action<string> onCallback) 
    {
        var playerNameData = dbRef.Child("players").Child(playerID).Child("name").GetValueAsync();

        yield return new WaitUntil(predicate: () => playerNameData.IsCompleted);

        if(playerNameData != null) 
        {
            DataSnapshot snapShot = playerNameData.Result;

            onCallback.Invoke(snapShot.Value.ToString());
        }
    }

    //score
    public IEnumerator GetScore(Action<int> onCallback)
    {
        var playerScoreData = dbRef.Child("players").Child(playerID).Child("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => playerScoreData.IsCompleted);

        if (playerScoreData.Result != null)
        {
            DataSnapshot snapShot = playerScoreData.Result;

            int score = snapShot.Value != null ? Convert.ToInt32(snapShot.Value) : 0;

            onCallback.Invoke(score);
        }
    }

    //game number
    public IEnumerator GetGameNumber(Action<int> onCallback)
    {
        var playerGameNumberData = dbRef.Child("players").Child(playerID).Child("gameNumber").GetValueAsync();
        
        yield return new WaitUntil(predicate: () => playerGameNumberData.IsCompleted);

        if (playerGameNumberData.Result != null)
        {
            DataSnapshot snapShot = playerGameNumberData.Result;

            int gameNumber = snapShot.Value != null ? Convert.ToInt32(snapShot.Value) : 0;

            onCallback.Invoke(gameNumber);
        }
    }


    public void GetPlayerInfo() 
    {
        //name
        StartCoroutine(GetName((string name) =>
        {
            NameText.text = "Player Name: " + name;
        }));

        //name
        StartCoroutine(GetScore((int score) =>
        {
            NameText.text = "\nPlayer Score: " + score;
        }));

        //game number
        StartCoroutine(GetGameNumber((int gameNumber) =>
        {
            NameText.text += "\nGame Number: " + gameNumber;
        }));
    }

}
//https://youtu.be/59RBOBbeJaA
