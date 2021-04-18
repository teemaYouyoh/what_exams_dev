using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class LocalScoreTableScript : MonoBehaviour
{
    
    public static ScoreTable ScoreTable = new ScoreTable(); // <ScoreTable> Class declaration: Assets/Classes/ScoreTable.cs
    public static Player player = new Player();

    void Start()
    {
        // Load data from JSON file 
        string fileName = "ScoreTable"; // '.json' format file name with high score table

        TextAsset asset = Resources.Load<TextAsset>(fileName);
        ScoreTable = JsonUtility.FromJson<ScoreTable>(asset.text);

        // Creating and initialization new player 
       

        // foreach (Player item in ScoreTable.Players) {
        //     print(item.id);
        //     print(item.name);
        //     print(item.score);
        // }

        // ScoreTable.save();
    }

    void Update()
    {
     
    }

    public static void addPlayer(Player data) {
        long currentUnixTime = System.DateTimeOffset.Now.ToUnixTimeSeconds(); // unix-time

        player.id = currentUnixTime * data.name.Length;
        player.name = data.name;
        player.score = data.score;

        ScoreTable.Players.Add(player);

        ScoreTable.save();
    }
}
