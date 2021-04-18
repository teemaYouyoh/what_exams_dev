using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordsTable : MonoBehaviour
{
    public Text nickname;
    public Text points;

    // Start is called before the first frame update
    void Start()
    {
        nickname.text =  DataHolder.nickname; 
        int playerPoints = DataHolder.points;
        int bonusPoints = Mathf.RoundToInt((float)(DataHolder.points + DataHolder.timerValue * 0.05));
        int totalPoints = playerPoints + bonusPoints;
        DataHolder.points = totalPoints;
        points.text =  totalPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
