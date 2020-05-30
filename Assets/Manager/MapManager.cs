using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    public const string MapPath = "Map/";
    public static List<Vector3> GetMapPath(string map_name)
    {
        TextAsset ta = Resources.Load<TextAsset>(MapPath + map_name);
        string text = ta.text;
        string[] pos = text.TrimEnd('\n').Split('\n');
        List<Vector3> List = new List<Vector3>();
        string[] pos_xy;
        for (int i = 0; i < pos.Length; i++)
        {
            pos_xy = pos[i].Split(',');
            Debug.Log(pos[i]);
            List.Add(new Vector3(int.Parse(pos[0]), 0, int.Parse(pos_xy[1])));
        }
        return List;
    }
}
