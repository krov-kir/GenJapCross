using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Play : MonoBehaviour
{
    public GameObject square;
    public int size;
    public new Camera camera;
    private List<List<bool>> _game_map;
    private List<List<bool>> _curr_map;
    public Text text;
    public void SaveToFile()
    {
        string dir = "Assets/Maps/Games/map";
        dir += (StaticClass.Num_map + ".txt");
        File.WriteAllText(dir, StaticClass.Size.ToString() + "\n");
        for (int i = 0; i < size; i++)
        {
            string s = "";
            for (int j = 0; j < size; j++)
            {
                if (StaticClass.Game_map[i][j])
                    s += "1";
                else
                    s += "0";
            }
            s += "\n";
            File.AppendAllText(dir, s);
        }
        for (int i = 0; i < size; i++)
        {
            string s = "";
            for (int j = 0; j < size; j++)
            {
                if (StaticClass.Map[i][j])
                    s += "1";
                else
                    s += "0";
            }
            s += "\n";
            File.AppendAllText(dir, s);
        }
    }

    public void BackToMenu()
    {
        SaveToFile();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    void Start()
    {
        text.GetComponent<Text>().enabled = false;
        size = StaticClass.Size;
        _game_map = StaticClass.Game_map;

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

    void Update()
    {
        _curr_map = StaticClass.Map;
        long count = size*size;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (_curr_map[i][j] == _game_map[i][j])
                    count--;
            }
        }

        if (count == 0)
            text.GetComponent<Text>().enabled = true;
        else
            text.GetComponent<Text>().enabled = false;
    }
}
