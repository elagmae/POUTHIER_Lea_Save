using System;
using System.IO;
using TMPro;
using UnityEngine;

public class JsonSaveHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeUI;
    [SerializeField]
    private TextMeshProUGUI _buttonClickedUI;
    [SerializeField]
    private TextMeshProUGUI _nameUI;

    private Data obj;
    private string _json;
    private string _content;
    private float _previousTime;

    // Stocker les valeurs de la classe à sauvegarder (ne pas oublier le seriazable).
    [Serializable]
    public class Data
    {
        public float Time;
        public int ButtonClicked;
        public string Name = "Léa";
    }

    private void Awake()
    {
        obj = new();

        // Si la file n'existe pas, on l'a créer en lui assignant un chemin.
        if (!File.Exists(Application.dataPath + "/SaveFileJson.json"))
        {
            File.Create(Application.dataPath + "/SaveFileJson.json");
            return;
        }

        // S'il n'est pas vide, on assigne les variables contenues dans la file aux variables internes.
        _json = File.ReadAllText(Application.dataPath + "/SaveFileJson.json");

        obj = JsonUtility.FromJson<Data>(_json);

        _nameUI.text = obj.Name;
        _buttonClickedUI.text = obj.ButtonClicked.ToString();

        _previousTime = obj.Time;
        _timeUI.text = _previousTime.ToString();
    }

    // On réecrit les variables à chaque appel de la méthode, autant dans l'UI que dans le fichier de sauvegarde.
    public void JsonMethod()
    {
        obj.Time = _previousTime + Time.realtimeSinceStartup;
        obj.ButtonClicked++;

        _nameUI.text = obj.Name;
        _buttonClickedUI.text = obj.ButtonClicked.ToString();
        _timeUI.text = obj.Time.ToString();
        _json = JsonUtility.ToJson(obj);

        File.WriteAllText(Application.dataPath + "/SaveFileJson.json", _json);
    }
}
