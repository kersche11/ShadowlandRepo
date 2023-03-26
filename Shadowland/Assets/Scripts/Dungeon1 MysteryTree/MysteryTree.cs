using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MysteryTree: MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private MysteryTree mysteryTree;
    [field:SerializeField] public TreeManager treeManager{get; private set;}


    private void Update()
    {
        if (health != null)
        {
           
            if(health.currentHealth<=0)
            {                               
                Destroy(this.gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        treeManager.CheckTreeOrder(mysteryTree);
    }

}
