
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneList : MonoBehaviour
{

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

    public Collider PlayerCollider { get; private set; }
    [field: SerializeField] public SceneNames selectedScene { get; private set; }

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
        if (PlayerCollider == null) { Debug.Log("Null"); return; }
        if (other != PlayerCollider) { Debug.Log("NotPlayer"); return; }

        Debug.Log("Load Scene");
        LoadScene();
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        
        PlayerCollider = player.GetComponent<Collider>();
       
    }

    public void LoadScene()
    {
        int Index = ((int)selectedScene);
        LevelManager.Instance.ChangeLevel(Index);
        SceneManager.LoadScene(Index);
        
    }


}



