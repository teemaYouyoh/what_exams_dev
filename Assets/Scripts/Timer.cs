using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeStart;
    public Text timerText;
    public float seconds;
    public float minutes;
    public string time0;
    public float minuteDuration = 59;
    public GameObject EndTimerPopup;

    // Start is called before the first frame update
    void Start()
    {
        timeStart = DataHolder.timerValue;

        timerText.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        minutes = Mathf.Floor(timeStart / minuteDuration);
        seconds = timeStart - (minutes * minuteDuration);
        if(seconds < 10)
        {
            time0 = "0";
        } else { time0 = ""; }

        if (minutes < 0)
        {
            EndTimerPopup.SetActive(true);
            Time.timeScale = 0f;
        }

        timerText.text = minutes.ToString() + ":" + time0 + Mathf.Round(seconds).ToString();
    }

    public void SetValue() {
        DataHolder.timerValue = timeStart;
    }

    public void onMuneBttnClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    // public void LoadScene(int Level) {
    //     Application.LoadLevel(Level);
    // }
}
