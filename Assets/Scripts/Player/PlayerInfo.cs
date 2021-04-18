using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{

    public string nickname;
    public InputField inputField;
    public Player player = new Player();
    public GameObject inputNameCavnvas;
    public GameObject exceptSituationsCanvas;
    public GameObject pauseMenuCanvas;

    void Start()
    {
        if (DataHolder.nickname == "" ) //|| DataHolder.nickname == "abc"
        {
            Debug.Log("1");
            Time.timeScale = 0f;
            inputNameCavnvas.SetActive(true);
            exceptSituationsCanvas.SetActive(false);
            pauseMenuCanvas.SetActive(false);
        }
    }

    public void enterName() {
        DataHolder.nickname = inputField.text;
        player.name = inputField.text;
        // LocalScoreTableScript.addPlayer(player);
        Debug.Log(DataHolder.nickname);

        Time.timeScale = 1f;
        inputNameCavnvas.SetActive(false);
        exceptSituationsCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(true);
    }
}
