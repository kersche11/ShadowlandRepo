using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneList : MonoBehaviour
{

    public enum SceneNames { 
        OpenWorld,
        DungeonOne,
        DungeonTwo,
        DungeonThree,
        DungeonFour,
        StartMenu,
        Intro
    }

    [SerializeField] Collider PlayerCollider;
    [field:SerializeField] public SceneNames selectedScene{get;private set;}

    //private Dictionary<string, int> sceneData = new Dictionary<string, int>
    //{
    //    {"OpenWorld",0 },
    //    {"DungeonOne",1 },
    //    {"DungeonTwo",2 },
    //    {"DungeonThree",3 },
    //    {"DungeonFour",4 },
    //    {"Startmenu",5 },
    //    {"Intro", 6}
    //};

    private void OnTriggerEnter(Collider other)
    {
            if (other != PlayerCollider) { return; }
        Debug.Log("ChangeScene!");
            LoadScene();
    }

    private void Start()
    {
        Debug.Log("Scene " + (int)selectedScene);
    }

    public void LoadScene()
    {
        int Index = ((int)selectedScene);
        SceneManager.LoadScene(Index);
    }


}



