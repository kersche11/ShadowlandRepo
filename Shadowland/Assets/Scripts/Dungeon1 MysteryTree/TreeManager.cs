using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
using static UnityEditor.PlayerSettings;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private MysteryTree[] correctTreeOrder;
    [SerializeField] private GameObject[] treePostions;
    [SerializeField] private Transform EnemySpawnPoint;
    [SerializeField] private Vector3 SpawnRangeMin;
    [SerializeField] private Vector3 SpawnRangeMax;
    [SerializeField] private GameObject EnemyList;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Trees;
    [SerializeField] private GameObject TreePositions;

    private float _xAxis;
    private float _yAxis;
    private float _zAxis;
    private int numberOfCorrectTrees = 0;
    private int numberOfFails = 0;
    private bool isFighting = false;



    private void Update()
    {
        if (isFighting)
        { 
            
            
        }
    }
    public void CheckTreeOrder(MysteryTree mysteryTree)
    {
        if (mysteryTree == null) { return; }
       
        int ordernumber = System.Array.IndexOf(correctTreeOrder, mysteryTree);

        if (ordernumber <= -1) {  return; }

        Debug.Log($"Ordernumber = {ordernumber}");

        if (ordernumber >= 5)
        {         
            SpawnEnemies(numberOfFails);
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
                SpawnEnemies(numberOfFails);
                numberOfCorrectTrees = 0;
                
            }
        }     
    }

    private void SpawnEnemies(int failCount)
    {
        isFighting = true;  
        numberOfFails++;
        Debug.Log("Spawn Enemies now!");
        Debug.Log("Fails:" + numberOfFails);

        for (int i = 0; i < numberOfFails; i++)
        {
            
            _xAxis = EnemySpawnPoint.position.x + Random.Range(SpawnRangeMin.x, SpawnRangeMax.x);
            _yAxis = EnemySpawnPoint.position.z + Random.Range(SpawnRangeMin.y, SpawnRangeMax.y);
            _zAxis = EnemySpawnPoint.position.z +Random.Range(SpawnRangeMin.z, SpawnRangeMax.z);

            Vector3 _randomPosition = new Vector3(_xAxis,_yAxis,_zAxis);

            Instantiate(Enemy, _randomPosition, Quaternion.identity);
           Enemy.TryGetComponent<NavMeshAgent>(out NavMeshAgent _agent);
            _agent = true;
        }

        DeaktivateTreeCollition();
        //ResetTreesRandom();
    }

    private void ResetTreesRandom()
    {
        foreach (var tree in correctTreeOrder)
        {
            //tree.transform.SetParent(Trees.transform);
            if (!tree.gameObject.activeSelf)
            {
                tree.gameObject.SetActive(true);
            }
           
            //tree.health.ResetHealth(1);
        }

        Debug.Log("Reset Trees Random");

        foreach (var tree in correctTreeOrder)
        {
            
            int rand= Random.Range(0, 9);
            

            while (treePostions[rand].transform.childCount >= 1)
            {
                Debug.Log("NEW Rand");
                rand = Random.Range(0, 9);
               
            }
            if (treePostions[rand].transform.childCount == 0)
            {
                Debug.Log("Set Tree to Postion");
                tree.transform.position = treePostions[rand].transform.position;
                tree.transform.SetParent(treePostions[rand].transform);
                if (!tree.gameObject.activeSelf)
                {
                    tree.gameObject.SetActive(true);
                }


                //tree.transform.position = treePostions[rand].transform.position;
            }


        }
    }

    private void DeaktivateTreeCollition()
    {
        foreach (var tree in correctTreeOrder)
        {
            tree.transform.SetParent(Trees.transform);
            if (tree.gameObject.activeSelf)
            {
                tree.gameObject.SetActive(false);
            }

            tree.health.ResetHealth(1);
        }
    }

}
