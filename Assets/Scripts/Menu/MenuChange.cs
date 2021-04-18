using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuChange : MonoBehaviour
{
    public string levelToLoad;
    public void onClickStart(string sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
