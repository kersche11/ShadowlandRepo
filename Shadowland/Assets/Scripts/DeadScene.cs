using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneList;

public class DeadScene : MonoBehaviour
{
    [SerializeField] GameObject deadCollider;

    [field: SerializeField] public SceneNames selectedScene { get; private set; }

    public enum SceneNames
    {
        StartMenu,
        OpenWorld,
        DungeonOne,
        DungeonTwo,
        DungeonThree,
        DungeonFour,
        Intro
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        int Index = ((int)selectedScene);
        LevelManager.Instance.ChangeLevel(Index);
        SceneManager.LoadScene(Index);

    }
}
