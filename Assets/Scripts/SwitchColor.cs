using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColor : MonoBehaviour
{
    public Sprite white;
    public Sprite blue;
    private int pos_x;
    private int pos_y;

    void Start()
    {
        pos_x = int.Parse(name.Split(',')[0]);
        pos_y = int.Parse(name.Split(',')[1]);
        if (StaticClass.Map[pos_x][pos_y])
            GetComponent<SpriteRenderer>().sprite = blue;
        else
            GetComponent<SpriteRenderer>().sprite = white;
    }
    void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>().sprite == white)
        {
            GetComponent<SpriteRenderer>().sprite = blue;
            StaticClass.Map[pos_x][pos_y] = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = white;
            StaticClass.Map[pos_x][pos_y] = false;
        }
    }
}
