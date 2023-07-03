using UnityEngine;


public class MysteryTree : MonoBehaviour
{
    [field: SerializeField] public Health health { get; private set; }
    [SerializeField] private MysteryTree mysteryTree;
    [field: SerializeField] public TreeManager treeManager { get; private set; }


    private void Update()
    {
        if (!treeManager.win)
        {
            if (health != null)
            {

                if (health.currentHealth <= 0)
                {
                    //Destroy(this.gameObject);
                    this.gameObject.SetActive(false);
                    treeManager.CheckTreeOrder(mysteryTree);

                }

            }
        }
        else { return; }



    }
    //private void OnDestroy()
    //{

    //}

}
