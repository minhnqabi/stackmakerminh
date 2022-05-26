using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageUI : SingletonMonoBehaviour<ManageUI>
{
    // Start is called before the first frame update
    public GameObject panelEndGame;
    public Button buttonNextLevel;
   
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ShowPanelEndGame()
    {
        panelEndGame.SetActive(true);
        buttonNextLevel.onClick.RemoveAllListeners();
        buttonNextLevel.onClick.AddListener(NextLevel);

    }
}
