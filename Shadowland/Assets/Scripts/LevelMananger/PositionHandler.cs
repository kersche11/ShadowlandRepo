using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerPosition PosDungeonOne;
    [SerializeField]
    private PlayerPosition PosDungeonTwo;
    [SerializeField]
    private PlayerPosition PosDungeonThree;
    [SerializeField]
    private GameObject PlayerRig;

    int[] levelInfo = new int[2];
   
    // Start is called before the first frame update
    void Start()
    {
        levelInfo = LevelManager.Instance.GetLevel();
        if (levelInfo.Length > 0 )
        {
            if (levelInfo[0] == 1 && levelInfo[1] == 2 )
            {
                PlayerRig.transform.position = PosDungeonOne.PlayerPos;
                PlayerRig.transform.rotation = PosDungeonOne.PlayerRot;
            }
            if (levelInfo[0] == 1 && levelInfo[1] == 3)
            {
                PlayerRig.transform.position = PosDungeonTwo.PlayerPos;
                PlayerRig.transform.rotation = PosDungeonTwo.PlayerRot;
            }
            if (levelInfo[0] == 1 && levelInfo[1] == 5)
            {
                PlayerRig.transform.position = PosDungeonThree.PlayerPos;
                PlayerRig.transform.rotation = PosDungeonThree.PlayerRot;
            }
        }
    }

}
