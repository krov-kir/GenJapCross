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
        var d = new DirectoryInfo("Assets/Maps/Creatures/");
        long i = 0;
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Equals(".txt"))
                i++;
        }
        

        if (InputSize.text == "")
            StaticClass.Size = 6;
        else
            StaticClass.Size = int.Parse(InputSize.text);
        GenMap();
        StaticClass.Num_map = (i + 1).ToString();
        SceneManager.LoadScene("Generator", LoadSceneMode.Single);
    }
    public void Load()
    {
        string dir = "Assets/Maps/Games/map";
        if (InputNumMap.text == "")
        {
            dir += "1.txt";
            StaticClass.Num_map = "1";
        }
        else
        {
            dir += InputNumMap.text.ToString() + ".txt";
            StaticClass.Num_map = InputNumMap.text.ToString();
        }
        string[] lines = File.ReadAllLines(dir);

        StaticClass.Size = int.Parse(lines[0]);
        GenMap();
        for (int i = 1; i <= StaticClass.Size; i++)
        {
            for (int j = 0; j < StaticClass.Size; j++)
            {
                if (lines[i][j] == '1')
                    StaticClass.Game_map[i - 1][j] = true;
                else
                    StaticClass.Game_map[i - 1][j] = false;
            }
        }
        for (int i = StaticClass.Size + 1; i <= 2 * StaticClass.Size; i++)
        {
            for (int j = 0; j < StaticClass.Size; j++)
            {
                if (lines[i][j] == '1')
                    StaticClass.Map[i - StaticClass.Size - 1][j] = true;
                else
                    StaticClass.Map[i - StaticClass.Size - 1][j] = false;
            }
        }
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
