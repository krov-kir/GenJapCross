using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class Play : MonoBehaviour
{
    public GameObject square;
    public GameObject num;
    public int size;
    public new Camera camera;
    public Text text;
    private List<List<bool>> _game_map;
    private List<List<bool>> _curr_map;
    private float firstX;
    private float firstY;
    [SerializeField] private MapData _MapData = new MapData();
    [SerializeField] private string saveFile;
    public void SaveToJson()
    {
        _MapData.__size = size.ToString();

        string __game__map = "";
        string __curr__map = "";
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (_game_map[i][j])
                    __game__map += '1';
                else
                    __game__map += '0';

                if (_curr_map[i][j])
                    __curr__map += '1';
                else
                    __curr__map += '0';
            }
        }
        _MapData.__game_map = __game__map;
        _MapData.__curr_map = __curr__map;

        File.WriteAllText(saveFile, JsonUtility.ToJson(_MapData));
    }

    public void BackToMenu()
    {
        SaveToJson();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    private void GetHorNum()
    {
        List<List<int>> hor_num = new List<List<int>>();
        for (int i = 0; i < size; i++)
        {
            int c = 0;
            List<int> row = new List<int>();
            for (int j = 0; j < size; j++)
            {
                if (_game_map[i][j])
                    c++;
                else
                {
                    if (c != 0)
                        row.Add(c);
                    c = 0;
                }
            }
            if (c != 0)
                row.Add(c);
            hor_num.Add(row);
        }

        for (int i = 0; i < size; i++)
        {
            int row_size = hor_num[i].Count;
            for (int j = 0; j < row_size; j++)
            {
                GameObject clone = Instantiate(num, new Vector3(firstX - row_size + j, firstY - i, 1.0f), Quaternion.identity);
                clone.name = "HorNum " + i.ToString() + "," + j.ToString();
                clone.GetComponent<TextMesh>().text = hor_num[i][j].ToString();
            }
        }
    }

    private void GetVerNum()
    {
        float max = 3.0f;
        List<List<int>> ver_num = new List<List<int>>();
        for (int j = 0; j < size; j++)
        {
            int c = 0;
            List<int> col = new List<int>();
            for (int i = 0; i < size; i++)
            {
                if (_game_map[i][j])
                    c++;
                else
                {
                    if (c != 0)
                        col.Add(c);
                    c = 0;
                }
            }
            if (c != 0)
                col.Add(c);
            ver_num.Add(col);
            if (col.Count > 0)
                max = col.Max() >= max ? col.Max() : max;
        }

        camera.orthographicSize = size / 2.0f + max;

        for (int i = 0; i < size; i++)
        {
            int col_size = ver_num[i].Count;
            for (int j = 0; j < col_size; j++)
            {
                GameObject clone = Instantiate(num, new Vector3(firstX + i, firstY + (col_size - j), 1.0f), Quaternion.identity);
                clone.name = "VerNum " + i.ToString() + "," + j.ToString();
                clone.GetComponent<TextMesh>().text = ver_num[i][j].ToString();
            }
        }
    }

    private void GetField()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject clone = Instantiate(square, new Vector3(firstX + j, firstY - i, 1.0f), Quaternion.identity);
                clone.name = i.ToString() + "," + j.ToString();
            }
        }
    }
    void Awake()
    {
        saveFile = Application.persistentDataPath + "/map" + StaticClass.Num_map + ".json";
    }
    void Start()
    {
        text.GetComponent<Text>().enabled = false;
        size = StaticClass.Size;
        _game_map = StaticClass.Game_map;

        firstX = -size / 2.0f + 0.5f;
        firstY = size / 2.0f - 0.5f;

        GetHorNum();
        GetVerNum();
        GetField();
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
