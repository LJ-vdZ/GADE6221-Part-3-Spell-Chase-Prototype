using Firebase.Database;
using Google.MiniJSON;
using System;
using System.Collections;
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

    public void CreatePlayer() 
    {
        Player newPlayer = new Player(Name.text);

        string json = JsonUtility.ToJson(newPlayer);

        dbRef.Child("players").Child(playerID).SetRawJsonValueAsync(json);
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

    public void GetPlayerInfo() 
    {
        //name
        StartCoroutine(GetName((string name) =>
        {
            NameText.text = "Player Name: " + name;
        }));

        //score
    }

}
//https://youtu.be/59RBOBbeJaA
