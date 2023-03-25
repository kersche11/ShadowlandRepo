using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private MysteryTree[] correctTreeOrder;
    private int numberOfCorrectTrees = 0;

    public void CheckTreeOrder(MysteryTree mysteryTree)
    {


        if (mysteryTree == null) { return; }
       
        int ordernumber = System.Array.IndexOf(correctTreeOrder, mysteryTree);
        if (ordernumber <= -1) {  return; }

        Debug.Log($"Ordernumber = {ordernumber}");

        if (ordernumber >= 5)
        {
            //Spawn Enemies
            Debug.Log("False Tree!");
        }

        else if (ordernumber < 5) 
        {
          if (ordernumber == numberOfCorrectTrees) 
            {

                Debug.Log("Correkt Tree");
                
                if (numberOfCorrectTrees==4)
                {
                    //Change to OpenWorld
                    Debug.Log("Treasure is spawning!");
                }
                numberOfCorrectTrees++;
            }
          else
            {
                //Spawn Enemies or Restart Dungeon
                Debug.Log("False Order! Restart!");
                numberOfCorrectTrees = 0;
            }
        }

        
    }
}
