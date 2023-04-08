using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCarryHandler : MonoBehaviour
{
    [field: SerializeField] public StoneCarryPosition StonePosition { get; private set; }
 

    private Stone stone;


    public StoneCarryHandler()
    {
        stone = null;
    }

    void Start()
    {
        stone = null;
    }



    public void GetStone()
    {
        
        stone.GetComponent<Rigidbody>().isKinematic = true;
        stone.GetComponent<Rigidbody>().detectCollisions = false;
        this.stone.transform.SetParent(StonePosition.transform);
        stone.transform.position = StonePosition.transform.position;
    }
    public void SetStone()
    {
        stone.GetComponent<Rigidbody>().isKinematic = false;
        stone.GetComponent<Rigidbody>().detectCollisions = true;
        this.stone.transform.SetParent(null);
        stone = null;
        
    }

    public void SetCurrentStone(Stone currentStone)
    {
        this.stone = currentStone;
    }


}
