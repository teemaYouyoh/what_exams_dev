using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Xml;

namespace DialogueSystem {
    
    public class QuestionsScript : MonoBehaviour
    {
        
        // Start is called before the first frame update
        void Start()
        {
            string fileName = "Questions";
            XmlElement xRoot = XmlDatabaseConnection.load(fileName);

            // обход всех узлов в корневом элементе
            foreach(XmlNode xnode in xRoot)
            {
                // получаем атрибут name
                // if(xnode.Attributes.Count>0)
                // {
                //     XmlNode attr = xnode.Attributes.GetNamedItem("name");
                //     if (attr!=null)
                //         Debug.Log(attr.Value);
                // }

                // обходим все дочерние узлы элемента question
                foreach(XmlNode childnode in xnode.ChildNodes)
                {
                    // если узел - text
                    if(childnode.Name=="text")
                    {
                        Debug.Log($"Вопрос: {childnode.InnerText}");
                    }
                    // если узел answer
                    if (childnode.Name == "answer")
                    {
                        Debug.Log($"Ответ: {childnode.InnerText}");
                    }
                    // если узел points
                    if (childnode.Name == "points")
                    {
                        Debug.Log($"Баллов: {childnode.InnerText}");
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}