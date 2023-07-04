using UnityEngine;

public class StoneCarryHandler : MonoBehaviour
{
    [field: SerializeField] public StoneCarryPosition StonePosition { get; private set; }

    [SerializeField]
    private Stone? stone;


    public StoneCarryHandler()
    {
       
    }

    void Start()
    {
       
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
        if (this.stone != null)
        {
            stone.GetComponent<Rigidbody>().isKinematic = false;
            stone.GetComponent<Rigidbody>().detectCollisions = true;
            this.stone.transform.SetParent(null);
            stone = null;
        }
       

    }

    public void SetCurrentStone(Stone currentStone)
    {
        this.stone = currentStone;
    }


}
