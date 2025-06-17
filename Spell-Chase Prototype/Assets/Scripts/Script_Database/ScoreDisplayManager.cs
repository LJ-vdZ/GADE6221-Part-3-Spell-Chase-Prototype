using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ScoreDisplayManager : MonoBehaviour
{
    [SerializeField]
    private Transform contentPanel; //assign ScrollView's Content panel

    [SerializeField]
    private GameObject playerInfoPrefab; //prefab with TMP_Text for player info

    private DatabaseReference dbRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        StartCoroutine(LoadAllPlayerData());
    }

    private IEnumerator LoadAllPlayerData()
    {
        var playerData = dbRef.Child("players").GetValueAsync();

        yield return new WaitUntil(predicate: () => playerData.IsCompleted);

        if (playerData.Result != null)
        {
            DataSnapshot snapshot = playerData.Result;

            List<PlayerData> playerDataList = new List<PlayerData>();

            foreach (var playerSnapshot in snapshot.Children)
            {
                string playerId = playerSnapshot.Key;

                string name = playerSnapshot.Child("playerName").Value?.ToString();

                int score = playerSnapshot.Child("score").Value != null ? Convert.ToInt32(playerSnapshot.Child("score").Value) : 0;

                int gameNumber = playerSnapshot.Child("gameNumber").Value != null ? Convert.ToInt32(playerSnapshot.Child("gameNumber").Value) : 0;

                playerDataList.Add(new PlayerData { Name = name, Score = score, GameNumber = gameNumber });
            }

            //sort score by descending so highest score is at the top
            playerDataList.Sort((a, b) => b.Score.CompareTo(a.Score));

            //instantiate UI elements
            foreach (var playersData in playerDataList)
            {
                GameObject playerInfoObj = Instantiate(playerInfoPrefab, contentPanel);

                TMP_Text textComponent = playerInfoObj.GetComponent<TMP_Text>();

                textComponent.text = $"Game: {playersData.GameNumber}, Name: {playersData.Name}, Score: {playersData.Score} \n--------------------------------";
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Score;
    public int GameNumber;
}
