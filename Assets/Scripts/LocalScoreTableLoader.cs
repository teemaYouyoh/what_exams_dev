using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Globalization;

public class LocalScoreTableLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        DataHolder.nickname = "";
        DataHolder.points = 0;
        DataHolder.timerValue = 295;
        DataHolder.doneTests = new List<Test>();
        DataHolder.crntScene = 0;

        StartCoroutine(SendLocalRecords());
        
    }

    private IEnumerator SendLocalRecords()
    {
        ScoreTable LocalScoreTable = new ScoreTable();
        string scoreTable = JsonDatabaseConnection.load("ScoreTable");
        LocalScoreTable = JsonUtility.FromJson<ScoreTable>(scoreTable);
        Debug.Log(LocalScoreTable);
        if (LocalScoreTable != null) {
            string data = "";
            string name = DataHolder.nickname;
            string score = DataHolder.points.ToString();
            string url = "http://seventh.touchit.space/addPlayer.php"; // Database server URL

            foreach (Player player in LocalScoreTable.Players) {
                List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

                wwwForm.Add(new MultipartFormDataSection("name", player.name));
                wwwForm.Add(new MultipartFormDataSection("score", player.score.ToString()));

                UnityWebRequest www = UnityWebRequest.Post(url, wwwForm);

                yield return www.SendWebRequest(); 
            }

            JsonDatabaseConnection.clean("ScoreTable");
        }
    }
}
