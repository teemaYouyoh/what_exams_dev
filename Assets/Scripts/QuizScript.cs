using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;
using System.Xml;

public class QuizScript : MonoBehaviour
{
    public string fileName;
    public string subjectName;

    public Canvas quizCanvas;
    public GameObject ExceptSituationsCanvas;

    public Text qText;
    public Text[] answersText;
    public Text pointsText;
    public Button[] answerBttns;

    public List<QuizQuestion> qList;

    QuizQuestion crntQ;
    public int totalPoints;
    int randQ;


    void OnEnable()
    {
        qList = new List<QuizQuestion>();
        GetQuestions();

        // startBtn.SetActive(false);
        QuestionsGenerate();
        DataHolder.isQuizStart = true;
        quizCanvas.gameObject.SetActive(true);
        quizCanvas.gameObject.GetComponentInChildren<Animation>().Play("QuizPanelFadeIn");
    }

    public void OnStartQuiz()
    {
        
    }

    void QuestionsGenerate()
    { 

        if (qList.Count > 0)
        {
            randQ = UnityEngine.Random.Range(0, qList.Count);
            crntQ = qList[randQ] as QuizQuestion;
            qText.text = crntQ.question;                   
            List<string> answers = new List<string>(crntQ.answers);

            for (int i = 0; i < crntQ.answers.Length; i++)
            {
                int rand = UnityEngine.Random.Range(0, answers.Count);
                answersText[i].text = answers[rand];
                answers.RemoveAt(rand);
            }
        } 
        else
        {
            pointsText.text = totalPoints.ToString();
            Transform quiztPanel = quizCanvas.transform.Find("QuizPanel");
            Transform resultPanel = quizCanvas.transform.Find("ResultPanel");
            quiztPanel.gameObject.SetActive(false);
            resultPanel.gameObject.SetActive(true);

            DataHolder.points += totalPoints;

            Test test = new Test();
            test.subject = subjectName;
            test.point = totalPoints;

            DataHolder.doneTests.Add(test);
        }
       
    }

    public void AnswersBttns(int index)
    {
        if (answersText[index].text.ToString() == crntQ.answers[0]) {
            totalPoints += qList[randQ].points;
            StartCoroutine(AnimBttns(true, index));
        } else {
            StartCoroutine(AnimBttns(false, index));
        }
    }

    IEnumerator AnimBttns(bool check, int index) {

        var crntBntColors = answerBttns[index].colors;
        var crntNormalColor = crntBntColors.normalColor;
        var crntHighlightedColor = crntBntColors.highlightedColor;
        
        if (check) {
            for (int i = 0; i < answerBttns.Length; i++) answerBttns[i].interactable = false;

            crntBntColors.normalColor = Color.green;
            crntBntColors.highlightedColor = Color.green;
            crntBntColors.disabledColor = Color.green;
            answerBttns[index].colors = crntBntColors;

        } else {
            crntBntColors.normalColor = Color.red;
            crntBntColors.highlightedColor = Color.red;
            crntBntColors.disabledColor = Color.red;
            answerBttns[index].colors = crntBntColors;
            
            for (int i = 0; i < answerBttns.Length; i++)
            {
                if (answerBttns[i].GetComponentInChildren<Text>().text == crntQ.answers[0]) {
                    var colors = answerBttns[i].colors;
                    colors.normalColor = Color.green;
                    colors.highlightedColor = Color.green;
                    colors.disabledColor = Color.green;
                    answerBttns[i].colors = colors;
                }
                answerBttns[i].interactable = false;
            }
        }
       
        
        yield return new WaitForSeconds(2);

        for (int i = 0; i < answerBttns.Length; i++)
        {
            answerBttns[i].interactable = true;

            var colors = answerBttns[i].colors;
            colors.normalColor = crntNormalColor;
            colors.highlightedColor = crntHighlightedColor;
            colors.disabledColor = crntHighlightedColor;
            answerBttns[i].colors = colors;
        }

        qList.RemoveAt(randQ);
        QuestionsGenerate();
    
    }

    void GetQuestions()
    {
        XmlElement xRoot = XmlDatabaseConnection.load(fileName);

        foreach(XmlNode xnode in xRoot)
        {
            QuizQuestion question = new QuizQuestion();

            if (xnode.Name == "question")
            {
                foreach(XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "text")
                    {
                        question.question = childnode.InnerText;
                    }
                    if (childnode.Name == "answers")
                    {
                        int answerN = 0;

                        foreach(XmlNode answerXnode in childnode.ChildNodes)
                        {
                            if(answerXnode.Name=="answer")
                            {
                                question.answers[answerN] = answerXnode.InnerText;
                                answerN++;
                            }
                        }
                    }
                    // если узел points
                    if (childnode.Name == "points")
                    {
                        question.points = Int32.Parse(childnode.InnerText);
                    }
                }
            }
            else
            {
                Debug.Log(xnode.Name);
            }

            qList.Add(question);
        }
    }

    public void FinishQuiz()
    {
        DataHolder.isQuizStart = false;
        quizCanvas.gameObject.SetActive(false);
        ExceptSituationsCanvas.SetActive(true);

        if (DataHolder.doneTests.Count == 3)
        {
            SceneManager.LoadScene("RecordsTable");
        }
    }
}

[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string[] answers = new string[4];
    public int points = 0;
}