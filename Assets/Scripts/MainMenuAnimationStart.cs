using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAnimationStart : MonoBehaviour
{
    public void StartClick()
    {
        SceneManager.LoadScene("SceneZETK");
    }
}
