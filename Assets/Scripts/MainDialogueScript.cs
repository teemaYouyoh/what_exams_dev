using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Xml;

namespace DialogueSystem {

    public class MainDialogueScript : MonoBehaviour
    {
        public Canvas quizCanvas;                           // Форма с вопросами

        public List<string> dialogueSequence;               // Список последовательности чередования диалогов и вопросов
        public Text qText;                                  // Текст вопроса
        public Text[] answersText;                          // Массив с ответами
        public int totalPoints;                             // Количество баллов за правильный ответ
        public List<DialogueQuestion> qList;                // Список вопросов
        public List<DialoguePhrase> pList;                  // Список фраз

        int dialogueIteration;                              // Итерация (этап) диалога
        int phraseNumber;                                   // Порядковый номер текущей фразы
        int questionNumber;                                 // Порядковый номер текущего вопроса
        
        [SerializeField] private string file;               // Название ".xml" файла с вопросами и диалогами 
        [SerializeField] private Transform parent;          // Контейнер для фраз диалога
        [SerializeField] private GameObject npc;            // Пример фраз npc
        [SerializeField] private GameObject player;         // Пример фраз player

        // Start is called before the first frame update
        void Start()
        {
            dialogueIteration = 0;
            phraseNumber = 0;
            questionNumber = 0;
            qList = new List<DialogueQuestion>();
            pList = new List<DialoguePhrase>();
            dialogueSequence =  new List<string>();

            getDialogue();
        }

        /*
         * Обработчик нажатия кнопки старта
         */
        public void onStartQuiz()
        {
            generateDialogue();
        }

        /*
         * Пошаговая генерация диалога
         */
        void generateDialogue()
        {
            if (dialogueSequence[dialogueIteration] == "phrase") // Если сейчас будет фраза
            {   
                // quizCanvas.gameObject.SetActive(false);
                Debug.Log(pList.Count);

                DialoguePhrase crntP = pList[phraseNumber] as DialoguePhrase;

                if (crntP.speaker == "npc")
                {
                    npc.GetComponent<DialogueLine>().input = crntP.phrase;
                    Instantiate(npc, parent);

                    if (dialogueSequence[dialogueIteration + 1] != null)
                    {
                        if (dialogueSequence[dialogueIteration + 1] == "phrase")
                        {
                            // generateDialogue();
                        }
                    }
                }
                else if (crntP.speaker == "player")
                {
                    player.GetComponent<DialogueLine>().input = crntP.phrase;
                    Instantiate(player, parent);
                }

                phraseNumber++;

                Debug.Log('p');
            }
            else if (dialogueSequence[dialogueIteration] == "question") // Если сейчас будет вопрос
            {
                Debug.Log(questionNumber);
                DialogueQuestion crntQ = qList[questionNumber] as DialogueQuestion;
                qText.text = crntQ.question;
                Debug.Log(crntQ.question);

                for (int i = 0; i < crntQ.answers.Length; i++)
                {
                    answersText[i].text = crntQ.answers[i];
                }

                quizCanvas.gameObject.SetActive(true);

                questionNumber++;

                Debug.Log('q');
                
            }

            dialogueIteration++;
        }

        /*
         * Переход на следующую итерацию (этап) диалога
         */
        void netxDialogueIteration() {
            generateDialogue();
        }

        /*
         * Обработчик нажатия кнопки ответа
         */
        public void answersBttns()
        {
            totalPoints += qList[dialogueIteration].points;

            // Проверка на последний вопрос
            if (qList.Count > 1)
            {
                generateDialogue();
            }
            else
            {
                Debug.Log(totalPoints);
            }
        }

        /*
         * Метод для получения диалога из ".xml" файла 
         */
        void getDialogue()
        {
            string fileName = "Questions";
            XmlElement xRoot = XmlDatabaseConnection.load(fileName);

            foreach(XmlNode xnode in xRoot)
            {
                DialogueQuestion questions = new DialogueQuestion();
                DialoguePhrase phrases = new DialoguePhrase();

                if (xnode.Name == "question")
                {

                    dialogueSequence.Add("question");

                    foreach(XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "text")
                        {
                            questions.question = childnode.InnerText;
                        }
                        if (childnode.Name == "answers")
                        {
                            int answerN = 0;

                            foreach(XmlNode answerXnode in childnode.ChildNodes)
                            {
                                if(answerXnode.Name=="answer")
                                {
                                    questions.answers[answerN] = answerXnode.InnerText;
                                    answerN++;
                                }
                            }
                        }
                        if (childnode.Name == "points")
                        {
                            questions.points = Int32.Parse(childnode.InnerText);
                        }
                    }

                    qList.Add(questions);
                }
                else if (xnode.Name == "phrase")
                {
                    dialogueSequence.Add("phrase");

                    if(xnode.Attributes.Count>0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("speaker");
                        
                        if (attr!=null) 
                        {
                            if (attr.Value == "npc")
                            {
                                phrases.speaker = "npc";
                               
                                foreach(XmlNode childnode in xnode.ChildNodes)
                                {
                                    // если узел - text
                                    if(childnode.Name=="text")
                                    {
                                        phrases.phrase = childnode.InnerText;
                                    }
                                }
                            } 
                            else if (attr.Value == "player")
                            {
                                phrases.speaker = "player";

                                foreach(XmlNode childnode in xnode.ChildNodes)
                                {
                                    // если узел - text
                                    if(childnode.Name=="text")
                                    {
                                        phrases.phrase = childnode.InnerText;
                                    }
                                }
                            }
                        }
                    }

                    pList.Add(phrases);
                }

            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

[System.Serializable]
public class DialogueQuestion
{
    public string question;                     // Текст вопроса
    public string[] answers = new string[4];    // Массив с ответами
    public int points = 0;                      // Количество баллов за правильный ответ
}

public class DialoguePhrase
{
    public string speaker;                      // Кто говорит фразу
    public string phrase;                       // Текст фразы
}