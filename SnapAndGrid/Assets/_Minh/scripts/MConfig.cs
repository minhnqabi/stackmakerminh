using UnityEngine;
using System.Text;
using System;
public static class MConfig
{

    public static string ANIM_JUMP = "Jump";
    public static string ANIM_DANCE = "Dance";
    public static string NAME_WALL = "dimian2";
    public static string TAG_WALL = "Wall";
    public static string TAG_BLOCK = "Block";
    public static string KEY_LV = "currentLv";
    public static string TAG_Wintrigger = "WinTrigger";
    public static void UpdateLevelUp()
    {
        PlayerPrefs.SetInt(KEY_LV, (PlayerPrefs.GetInt(KEY_LV, 0) + 1));
    }

    public static string ArrayToString(int[,] data)
    {
        StringBuilder sb = new StringBuilder();
        for (int a = 0; a < data.GetLength(0); a++)
        {
            for (int b = 0; b < data.GetLength(1); b++)
            {
                sb.Append(data[a, b].ToString() + ",");
            }
            sb.Append(".");
        }
        return sb.ToString();
    }
    public static int[,] ArrayFromString(string toConvert)
    {
        string[] rows = toConvert.Split('.');
        string[] elements = rows[0].Split(',');

        int[,] result = new int[rows.Length, elements.Length];

        for (int a = 0; a < rows.Length-1; a++)
        {
            string[] items = rows[a].Split(',');

            for (int b = 0; b < items.Length-1; b++)
            {
                result[a, b] = int.Parse(items[b]);
                //Debug.LogError("result"+a+","+b+":"+items[b]);
            }
        }

        return result;
    }
}