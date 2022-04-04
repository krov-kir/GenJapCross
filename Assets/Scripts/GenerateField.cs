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
    public ScrollRect scr;

    private void Awake()
    {
        StaticClass.Num_map = 0.ToString();
    }
    private void GenMap()
    {
        int size = StaticClass.Size;
        for (int i = 0; i < size; i++)
        {
            List<bool> row = new List<bool>();
            for (int j = 0; j < size; j++)
            {
                row.Add(false);
            }
            StaticClass.Map.Add(row);
        }
    }
    public void Generate()
    {
        var d = new DirectoryInfo("Assets/Maps/");
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
        string dir = "Assets/Maps/map";
        if (InputNumMap.text == "")
            dir += "1.txt";
        else
            dir += InputNumMap.text.ToString() + ".txt";
        string[] lines = File.ReadAllLines(dir);
        StaticClass.Size = int.Parse(lines[0]);
        GenMap();
        for (int i = 1; i <= StaticClass.Size; i++)
        {
            for (int j = 0; j < StaticClass.Size; j++)
            {
                if (lines[i][j] == '1')
                    StaticClass.Map[i - 1][j] = true;
                else
                    StaticClass.Map[i - 1][j] = false;
            }
        }
        SceneManager.LoadScene("Generator", LoadSceneMode.Single);
    }
}
