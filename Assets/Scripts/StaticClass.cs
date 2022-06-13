using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class StaticClass
{
    private static int size;
    private static List<List<bool>> map = new List<List<bool>>();
    private static List<List<bool>> game_map = new List<List<bool>>();
    private static string num_map;

    public static int Size { get => size; set => size = value; }
    public static List<List<bool>> Map { get => map; set => map = value; }
    public static string Num_map { get => num_map; set => num_map = value; }
    public static List<List<bool>> Game_map { get => game_map; set => game_map = value; }
}
