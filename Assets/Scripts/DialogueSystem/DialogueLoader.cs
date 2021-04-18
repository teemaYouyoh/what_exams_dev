using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Xml;

namespace DialogueSystem {
    
    public class DialogueLoader : MonoBehaviour
    {
        [SerializeField] private string file;
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject npc;
        [SerializeField] private GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            XmlElement xRoot = XmlDatabaseConnection.load(file);

            // обход всех узлов в корневом элементе
            foreach(XmlNode xnode in xRoot)
            {
                // получаем атрибут speaker
                if(xnode.Attributes.Count>0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("speaker");
                    
                    if (attr!=null) 
                    {

                        if (attr.Value == "npc")
                        {
                            foreach(XmlNode childnode in xnode.ChildNodes)
                            {
                                // если узел - text
                                if(childnode.Name=="text")
                                {
                                    string text = childnode.InnerText;
                                    npc.GetComponent<DialogueLine>().input = text;
                                    Instantiate(npc, parent);
                                }
                            }
                        } 
                        else if (attr.Value == "player")
                        {
                           foreach(XmlNode childnode in xnode.ChildNodes)
                            {
                                // если узел - text
                                if(childnode.Name=="text")
                                {
                                    string text = childnode.InnerText;
                                    player.GetComponent<DialogueLine>().input = text;
                                    Instantiate(player, parent);
                                }
                            }
                        }
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