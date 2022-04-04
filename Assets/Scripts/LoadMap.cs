using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour
{
    public void OnClickLoad()
    {
        string dir = "Assets/Maps/map";
        dir = dir + this.name + ".txt";

    }
}
