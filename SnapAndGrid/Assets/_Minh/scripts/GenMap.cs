using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using System.Linq;

public class GenMap : SerializedMonoBehaviour
{
    // Start is called before the first frame update
    public int row, colum;
    public float pice;
    [HideInInspector] public Transform lvContain;

    // [TableMatrix]
    [HideInInspector] public int[,] map = new int[100, 100];
    public GameObject wall, earnStack, fillStack;


    void Start()
    {
        map = new int[row, colum];
        Debug.Log(map.Length);
        customCellDraw = new int[25, 25];

    }



    public void GenMapPrefab()
    {



        while (lvContain.childCount > 0)
        {
            DestroyImmediate(lvContain.GetChild(0).gameObject);
        }

        Debug.Log(map.Length);
        for (int i = 0; i < colum; i++)
        {
            for (int j = 0; j < row; j++)
            {
                if (map[i, j] == 0)
                {
                    GameObject objwall = Instantiate(wall, new Vector3(j * pice, 0, i * pice), wall.transform.rotation);
                    objwall.transform.parent = lvContain;

                }
                if (map[i, j] == 1)
                {
                    GameObject objwall = Instantiate(earnStack, new Vector3(j * pice, 0, i * pice), wall.transform.rotation);
                    objwall.transform.parent = lvContain;

                }
                if (map[i, j] == 2)
                {
                    GameObject objwall = Instantiate(fillStack, new Vector3(j * pice, 0, i * pice), wall.transform.rotation);
                    objwall.transform.parent = lvContain;

                }

            }
        }
    }

    [TableMatrix(DrawElementMethod = "DrawCell")]
    public int[,] customCellDraw = new int[25, 25];
    private int DrawCell(Rect rect, int value = -1)
    {

        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            value++;
            if (value > 3) value = 0;
            GUI.changed = true;
            Event.current.Use();

        }
        Color _color = new Color();
        if (value == 1)
        {
            _color = Color.white;
        }
        if (value == 2)
        {
            _color = Color.yellow;
        }
        if (value == 3)
        {
            _color = Color.gray;
        }
        EditorGUI.DrawRect(rect.Padding(1), _color);
        EditorGUI.LabelField(rect, value.ToString());

        return value;
    }
    [Button]
    public void GenMap2()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        GameObject LevelData = new GameObject("LevelData");
        LevelData.AddComponent<LevelData>();
        LevelData.transform.parent = transform;
        this.lvContain = LevelData.transform;


        for (int a = 0; a < 25; a++)
        {
            for (int b = 0; b < 25; b++)
            {
                Debug.Log(a.ToString() +
                "-" + b.ToString() + ":" + customCellDraw[a, b].ToString());
            }
        }


        while (lvContain.childCount > 0)
        {
            DestroyImmediate(lvContain.GetChild(0).gameObject);
        }

        Debug.Log(map.Length);
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 25; j++)
            {
                if (customCellDraw[i, j] == 1)
                {
                    GameObject objwall = Instantiate(wall, new Vector3(j * pice, 0, i * pice), wall.transform.rotation);
                    objwall.transform.parent = lvContain;

                }
                if (customCellDraw[i, j] == 2)
                {
                    GameObject objwall = Instantiate(earnStack, new Vector3(j * pice, 0, i * pice), wall.transform.rotation);
                    objwall.transform.parent = lvContain;

                }
                if (customCellDraw[i, j] == 3)
                {
                    GameObject objwall = Instantiate(fillStack, new Vector3(j * pice, 0, i * pice), wall.transform.rotation);
                    objwall.transform.parent = lvContain;

                }

            }
        }

        lvContain.GetComponent<LevelData>().mapData = MConfig.ArrayToString(customCellDraw);
    }

    [Button]
    public void ResetMap()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 25; j++)
            {
                customCellDraw[i, j] = 0;
            }
        }

    }
    public LevelData dataContain;

    [Button]
    public void LoadMapData()
    {
        int[,] loadedData = MConfig.ArrayFromString(dataContain.mapData);
        customCellDraw = loadedData;
        this.GenMap2();

    }

    public void CreateMap()
    {


    }

}
