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
    [SerializeField] private MapData _MapData = new MapData();
    [SerializeField] private string saveFile;

    public void SaveToJson()
    {
        _MapData.__size = size.ToString();

        string _game_map = "";
        string _curr_map = "";
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (StaticClass.Map[i][j])
                    _game_map += '1';
                else
                    _game_map += '0';

                if (StaticClass.Game_map[i][j])
                    _curr_map += '1';
                else
                    _curr_map += '0';
            }
        }
        _MapData.__game_map = _game_map;
        _MapData.__curr_map = _curr_map;

        File.WriteAllText(saveFile, JsonUtility.ToJson(_MapData));
    }

    public void BackToMenu()
    {
        SaveToJson();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    void Awake()
    {
        saveFile = Application.persistentDataPath + "/map" + StaticClass.Num_map + ".json";
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