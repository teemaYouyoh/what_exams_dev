using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExceptSituations : MonoBehaviour
{
  public int time = 0;    
  public GameObject afkPanel;
  public GameObject controlTab;
          
    void FixedUpdate () {

        if (!Input.anyKey)
        {
            time = time + 1;
        }
        else 
        {
            time = 0;
            afkPanel.SetActive(false);
        }
        
        if (time == 700)
        {
            afkPanel.SetActive(true);
            time = 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            controlTab.SetActive(true);
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controlTab.SetActive(false);
        }
    }
}
