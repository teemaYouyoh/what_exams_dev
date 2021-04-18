using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject controlsTab;
    public GameObject pointsTab;

    public Text pointH;
    public Text pointP;
    public Text pointW;


    // Update is called once per frame
    void Update()
    {
        
        if (DataHolder.nickname != "")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }

                controlsTab.SetActive(false);
                pointsTab.SetActive(false);
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }
    public void PointsTab()
    {
        foreach (Test test in DataHolder.doneTests)
        {
            switch (test.subject)
            {
                case "History" : pointH.text = test.point.ToString(); break;
                case "Programming" : pointP.text = test.point.ToString(); break;
                case "WebDev" : pointW.text = test.point.ToString(); break;
                default: break;
            }
        }

        pauseMenuUI.SetActive(false);
        pointsTab.SetActive(true);
        GameIsPaused = true;
    }
    public void MenuGame()
    {
        pauseMenuUI.SetActive(false);
        controlsTab.SetActive(true);
        GameIsPaused = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
