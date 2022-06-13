using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class CreateField : MonoBehaviour
{
    public GameObject square;
    public int size;
    public new Camera camera;

    public void SaveToFile()
    {
        string dir = "Assets/Maps/Creatures/map";
        dir += (StaticClass.Num_map + ".txt");
        var f = new FileInfo(dir);
        f.Create();

        if (File.Exists(dir))
        {
            var _curr_map = StaticClass.Map;
            StreamWriter sw;
            sw = f.AppendText();
            sw.WriteLine(StaticClass.Size.ToString());
            //File.WriteAllText(dir, StaticClass.Size.ToString() + "\n");
            for (int i = 0; i < size; i++)
            {
                string s = "";
                for (int j = 0; j < size; j++)
                {
                    if (_curr_map[i][j])
                        s += "1";
                    else
                        s += "0";
                }
                //s += "\n";
                sw.WriteLine(s);
                //File.AppendAllText(dir, s);
            }
            for (int i = 0; i < size; i++)
            {
                string s = "";
                for (int j = 0; j < size; j++)
                    s += "0";
                //s += "\n";
                sw.WriteLine(s);
                //File.AppendAllText(dir, s);
            }
            sw.Close();
        }
        else
        {
            Debug.Log("Not exists");
        }
    }

    public void BackToMenu()
    {
        SaveToFile();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    void Start()
    {
        size = StaticClass.Size;
        float firstX = -size / 2.0f + 0.5f;
        float firstY = size / 2.0f - 0.5f;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject clone = Instantiate(square, new Vector3(firstX + j, firstY - i, 1.0f), Quaternion.identity);
                clone.name = i.ToString() + "," + j.ToString();
            }
        }
        camera.orthographicSize = size / 2.0f + 2.0f;
    }
}
