using System.Globalization;
using System.Xml;
using TMPro;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeUI;
    [SerializeField]
    private TextMeshProUGUI _buttonClickedUI;
    [SerializeField]
    private TextMeshProUGUI _nameUI;

    private float _previousTime;
    private float _time;
    private int _buttonClicked;
    private string _name = "Léa";

    // Exercice 3

    private void Awake()
    {
        XmlDocument xmlDocument = new();
        if (!System.IO.File.Exists("Assets /" + "SaveFile.xml"))
        {
            return;
        }
        xmlDocument.LoadXml(System.IO.File.ReadAllText("Assets /" + "SaveFile.xml"));

        string key;
        string value;

        foreach (XmlNode node in xmlDocument.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;
            switch (key)
            {
                case "Nom":
                    _name = value;
                    break;

                case "Time":
                    _previousTime = float.Parse(value, CultureInfo.InvariantCulture);
                    break;

                case "ButtonClicked":
                    _buttonClicked = int.Parse(value);
                    break;
            }
        }

        _timeUI.text = _previousTime.ToString(CultureInfo.InvariantCulture);
        _nameUI.text = _name;
        _buttonClickedUI.text = _buttonClicked.ToString();
    }

    // Exercice 1

    public void XmlMethod()
    {
        XmlWriterSettings xmlWriterSettings = new()
        {
            NewLineOnAttributes = true,
            Indent = true
        };

        XmlWriter xmlWriter = XmlWriter.Create("Assets /" + "SaveFile.xml", xmlWriterSettings);

        _time = _previousTime + Time.realtimeSinceStartup;
        _buttonClicked++;

        _timeUI.text = _time.ToString(CultureInfo.InvariantCulture);
        _nameUI.text = _name;
        _buttonClickedUI.text = _buttonClicked.ToString();

        WriteInXml(xmlWriter, _name, _time, _buttonClicked);
    }

    // Exercice 2

    public void WriteInXml(XmlWriter writer, string name, float time, int buttonClicked)
    {
        writer.WriteStartDocument();
        writer.WriteStartElement("Save");

        writer.WriteStartElement("Nom");
        writer.WriteString(name);
        writer.WriteEndElement();

        writer.WriteStartElement("Time");
        writer.WriteValue(time);
        writer.WriteEndElement();

        writer.WriteStartElement("ButtonClicked");
        writer.WriteValue(buttonClicked);
        writer.WriteEndElement();

        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Close();
    }
}
