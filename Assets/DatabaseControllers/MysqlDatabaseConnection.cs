using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Globalization;

public class MysqlDatabaseConnection : MonoBehaviour
{

    public GameObject scoreTableItem;
    public GameObject scoreTableItemError;
    public GameObject scoreTableContent;
    
    void Start()
    {
        StartCoroutine(Send());
    }

    private IEnumerator Send() {
        string data = "";

       

        string name = DataHolder.nickname;
        string score = DataHolder.points.ToString();
        string url = "http://seventh.touchit.space/addPlayer.php"; // Database server URL

        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

        if (name != "" || name != null)
        {
            wwwForm.Add(new MultipartFormDataSection("name", name));
            wwwForm.Add(new MultipartFormDataSection("score", score));

            UnityWebRequest www_add = UnityWebRequest.Post(url, wwwForm);

            yield return www_add.SendWebRequest();
        }

        url = "http://seventh.touchit.space/"; // Database server URL
        wwwForm = new List<IMultipartFormSection>();

        UnityWebRequest www = UnityWebRequest.Post(url, wwwForm);

        www = UnityWebRequest.Post(url, wwwForm);

        yield return www.SendWebRequest();

        data = www.downloadHandler.text;

        ScoreTable GlobalScoreTable = new ScoreTable(); // <ScoreTable> Class declaration: Assets/Classes/ScoreTable.cs
        Debug.Log(data);
        GlobalScoreTable = JsonUtility.FromJson<ScoreTable>(data);
        Debug.Log(GlobalScoreTable);
        
        if (GlobalScoreTable != null) {
            foreach (Player player in GlobalScoreTable.Players) {
                scoreTableItem.transform.Find("Id").GetComponent<Text>().text = player.id.ToString();
                scoreTableItem.transform.Find("Name").GetComponent<Text>().text = player.name;
                scoreTableItem.transform.Find("Score").GetComponent<Text>().text = player.score.ToString();
                Instantiate(scoreTableItem.transform, scoreTableContent.transform);
            }    
        } else {
            ScoreTable LocalScoreTable = new ScoreTable(); // <ScoreTable> Class declaration: Assets/Classes/ScoreTable.cs
            Player player = new Player();

            string scoreTable = JsonDatabaseConnection.load("ScoreTable");

            if (scoreTable != "")
            {
                LocalScoreTable = JsonUtility.FromJson<ScoreTable>(scoreTable);
                Debug.Log(LocalScoreTable.Players);
            }
           
            long currentUnixTime = System.DateTimeOffset.Now.ToUnixTimeSeconds(); // unix-time

            player.name = DataHolder.nickname;
            player.score = DataHolder.points;
            player.id = currentUnixTime * player.name.Length;

            LocalScoreTable.Players.Add(player);
            LocalScoreTable.save();
            
            scoreTableContent.SetActive(false);
            scoreTableItemError.SetActive(true);
        }

        scoreTableItem.SetActive(false);
    }
}
