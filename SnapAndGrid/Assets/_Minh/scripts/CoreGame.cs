using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGame : SingletonMonoBehaviour<CoreGame>
{
    public MapDataScriptAble dataLevel;
    public Transform mapContainer;
    public GameObject endGameObject;
    // Start is called before the first frame update
    public int currentLv;
    void Start()
    {
           if (!PlayerPrefs.HasKey(MConfig.KEY_LV))
        {
            currentLv = 0;
            PlayerPrefs.SetInt(MConfig.KEY_LV,currentLv);
        }
        else
        {
            currentLv = PlayerPrefs.GetInt(MConfig.KEY_LV, 0);
        }
        this.Setup();
    }


    public void Setup()
    {
        this.GenMap();
        endGameObject.transform.position = Endpoint.instance.transform.position;
        PlayerManage.instance.transform.position = Startpoint.instance.transform.position;

    }
    public void GenMap()
    {
        Instantiate(dataLevel.LevelDatas[currentLv].mapPrefab, mapContainer);


    }
}
