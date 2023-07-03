using System.Collections.Generic;
using UnityEngine;

public class ItemTargeter : MonoBehaviour
{
    private List<Stone> stones = new List<Stone>();
    private List<Item> items = new List<Item>();

    private Camera mainCamera;
    public Stone CurrentStone { get; private set; }
    public Item CurrentItem { get; private set; }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Stone>(out Stone stone))
        {
            stones.Add(stone);
        }


        if (other.TryGetComponent<Item>(out Item item))
        {
            items.Add(item);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Stone>(out Stone stone))
        {
            RemoveStone(stone);
        }

        if (other.TryGetComponent<Item>(out Item item))
        {
            RemoveItem(item);
        }

    }


    //Selected das näheste Target welches sich in der TargetListe befindet
    public bool SelectStone()
    {
        if (stones.Count == 0) { return false; }

        Stone closestStone = null;
        float closestStoneDistance = Mathf.Infinity;

        //Durchlaufe alle Targets in der Liste
        foreach (Stone stone in stones)
        {
            //Suche Targetpostion am Bildschirm (HEIGHT 0-1, WIDTH 0-1 ist am Bildschirm)
            Vector2 viewPosition = mainCamera.WorldToViewportPoint(stone.transform.position);

            //Check ob das Target ausserhalb des Bildschirmes ist
            if (viewPosition.y < 0 || viewPosition.y > 1 || viewPosition.x < 0 || viewPosition.x > 1)
            {
                continue;
            }

            //Better Version not workng :D
            //if (target.GetComponentInChildren<Renderer>().isVisible)
            //{
            //    continue;
            //}

            //Errechne die Distanz von der Bildschírmmitte zum Target
            Vector2 distanceToCenter = viewPosition - new Vector2(0.5f, 0.5f);

            //Wenn die Distanz kleiner als die bisherige näheste Distanz ist
            //wird das neue Target zum "Closest Target"
            if (distanceToCenter.sqrMagnitude < closestStoneDistance)
            {
                closestStone = stone;
                closestStoneDistance = distanceToCenter.sqrMagnitude;
            }

        }

        if (closestStone == null) { return false; }


        CurrentStone = closestStone;



        return true;
    }



    //Zerstörtes Target wird aus der Targeterliste und aus der Cinemachineliste entfernt
    private void RemoveStone(Stone stone)
    {
        //Entfernen aus targets liste
        stones.Remove(stone);
    }
    private void RemoveItem(Item item)
    {
        //Entfernen aus targets liste
        items.Remove(item);
    }

    public Stone GetCurrentStone()
    {
        return CurrentStone;
    }


}

