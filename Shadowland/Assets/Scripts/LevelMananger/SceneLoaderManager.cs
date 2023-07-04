using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneLoaderManager : MonoBehaviour
{
    [SerializeField] private GameObject SceneLoaderDungeonOne;
    [SerializeField] private GameObject SceneLoaderDungeonTwo;
    [SerializeField] private GameObject SceneLoaderDungeonThree;
    [SerializeField] private TextMeshProUGUI DiamondCount;


    private int diamondCount=0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        diamondCount = PlayerPrefs.GetInt(LevelManager.Instance.scorekey);
        if (diamondCount == 0 )
        {
            SceneLoaderDungeonOne.SetActive(true);
            SceneLoaderDungeonTwo.SetActive(false);
            SceneLoaderDungeonThree.SetActive(false);
           
        }
        else if (diamondCount == 1)
        {
            SceneLoaderDungeonOne.SetActive(false);
            SceneLoaderDungeonTwo.SetActive(true);
            SceneLoaderDungeonThree.SetActive(false);
        }
        else if (diamondCount == 2)
        {
            SceneLoaderDungeonOne.SetActive(false);
            SceneLoaderDungeonTwo.SetActive(false);
            SceneLoaderDungeonThree.SetActive(true);
        }
        else
        {
            SceneLoaderDungeonOne.SetActive(true);
            SceneLoaderDungeonTwo.SetActive(true);
            SceneLoaderDungeonThree.SetActive(false);

        }
        DiamondCount.text = "Diamonds: " + diamondCount + "/2";
    }

    
}
