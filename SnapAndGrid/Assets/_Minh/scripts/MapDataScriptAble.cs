using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "LevelData", order = 0)]
public class MapDataScriptAble : ScriptableObject
{
    // Start is called before the first frame update
   
   public List<MapData> LevelDatas;
}
[System.Serializable]
public class MapData
{
     public int[,] mapData=new int[25,25];
     public GameObject mapPrefab;
}
