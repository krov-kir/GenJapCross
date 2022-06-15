using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GenerateField : MonoBehaviour
{
    public InputField InputSize;
    public InputField InputNumMap;
    [SerializeField] private string saveFile;
    [SerializeField] private MapData _MapData = new MapData();

    private void Awake()
    {
        StaticClass.Num_map = 0.ToString();
    }
    private void GenMap()
    {
        int size = StaticClass.Size;
        for (int i = 0; i < size; i++)
        {
            List<bool> row1 = new List<bool>();
            List<bool> row2 = new List<bool>();
            for (int j = 0; j < size; j++)
            {
                row1.Add(false);
                row2.Add(false);
            }
            StaticClass.Map.Add(row1);
            StaticClass.Game_map.Add(row2);
        }
    }
    public void Generate()
    {
        var d = new DirectoryInfo(Application.persistentDataPath);
        long i = 0;
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Equals(".json"))
                i++;
        }

        if (InputSize.text == "")
            StaticClass.Size = 10;
        else
            StaticClass.Size = int.Parse(InputSize.text);
        GenMap();
        StaticClass.Num_map = (i + 1).ToString();
        SceneManager.LoadScene("Generator", LoadSceneMode.Single);
    }
    public void Load()
    {
        saveFile = Application.persistentDataPath + "/map";
        if (InputNumMap.text == "")
        {
            saveFile += "1.json";
            StaticClass.Num_map = "1";
        }
        else
        {
            saveFile += InputNumMap.text.ToString() + ".json";
            StaticClass.Num_map = InputNumMap.text.ToString();
        }
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            _MapData = JsonUtility.FromJson<MapData>(fileContents);
        }
        else
            Debug.Log("File doesnt exist!");

        StaticClass.Size = int.Parse(_MapData.__size);
        GenMap();
        for (int i = 0; i < StaticClass.Size; i++)
        {
            for (int j = 0; j < StaticClass.Size; j++)
            {
                if (_MapData.__game_map[i * StaticClass.Size + j] == '1')
                    StaticClass.Game_map[i][j] = true;
                else
                    StaticClass.Game_map[i][j] = false;
            }
        }
        for (int i = 0; i < StaticClass.Size; i++)
        {
            for (int j = 0; j < StaticClass.Size; j++)
            {
                if (_MapData.__curr_map[i * StaticClass.Size + j] == '1')
                    StaticClass.Map[i][j] = true;
                else
                    StaticClass.Map[i][j] = false;
            }
        }
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}