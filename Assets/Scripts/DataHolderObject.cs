using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataHolderObject : MonoBehaviour
{

    public Text points;

    void Start() {
        points.text = DataHolder.points.ToString();
    }
}


public class DataHolder
{
    public static string nickname = "aaa";
    public static int points = 0;
    public static float timerValue = 295;
    public static List<Test> doneTests = new List<Test>();
    public static int crntScene;
    public static bool isQuizStart = false;
}

public class Test
{
    public string subject;
    public int point;
}
