using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreTable 
{
    public List<Player> Players = new List<Player>(); //  <Player> Сlass declaration: Assets/Classes/Player.cs

    public void save() {
        string file = "ScoreTable";
        string data = JsonUtility.ToJson(this, true); 

        JsonDatabaseConnection.save(file, data);
    }
}
