using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public void SetField(int size, GameObject square, float firstX, float firstY)
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
}
