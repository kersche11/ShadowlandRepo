using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public string scorekey = "Score";
    private int actualLevel = 0;
    private int lastLevel;

    private int diamondCount = 0;


    public void ChangeLevel(int index)
    {
        lastLevel = actualLevel;
        actualLevel = index;

        Debug.Log("ActualLevel: " + actualLevel);
        Debug.Log("LastLevel: " + lastLevel);
    }

    public int[] GetLevel()
    {
       int[] levels = { actualLevel, lastLevel};
        return levels;
    }

    public void IncreaseDiamondCount()
    {
        diamondCount++;
        PlayerPrefs.SetInt(scorekey, diamondCount);

    }
    public int GetDiamondCount()
    {
        diamondCount = PlayerPrefs.GetInt(scorekey);
        return diamondCount;
    }
    public void ResetDiamondCount()
    {
        diamondCount= 0;
    }
}
