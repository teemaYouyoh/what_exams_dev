using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class XmlDatabaseConnection
{
    public static XmlElement load(string path) {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(Application.dataPath+"/Resources/" + path + ".xml");
        XmlElement xRoot = xDoc.DocumentElement;

        return xRoot;
    }

}
