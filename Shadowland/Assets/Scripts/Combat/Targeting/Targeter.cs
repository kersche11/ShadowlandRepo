using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Der Targeter Added Enemies(Targets) indei Target Liste sobald diese in den SphereCollider eintreten
//und löscht sie wieder sobald sie aus dem SphereCollider draussen sind.
public class Targeter : MonoBehaviour
{
    //Referenz zur CmTargetGroup um die TargetListe zu adden
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    private Camera mainCamera;

    private List<Target> targets=new List<Target>();
    public Target CurrentTarget { get; private set; }

   

    private void Start()
    {
       
        mainCamera = Camera.main;
    }



    private void OnTriggerEnter(Collider other)
    {
        //Target target = other.GetComponent<Target>();
        //if (target == null) { return; }
        //targets.Add(target);

        //Andere Version same effect:

        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Add(target);

        //Wenn ein Target zerstört wird löschen wir es aus der Liste
        target.DestroyEvent += RemoveTarget;


    }
    private void OnTriggerExit(Collider other)
    {
        //Target target = other.GetComponent<Target>();
        //if(target == null) { return; }
        //targets.Remove(target);

        //Other Version:
        if(!other.TryGetComponent<Target>(out Target target)) {  return; }  

        //Entfernt Targets aus CM Group und Targeter Liste
        RemoveTarget(target);
    }

    //Selected das näheste Target welches sich in der TargetListe befindet
    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        //Durchlaufe alle Targets in der Liste
        foreach (Target target in targets)
        {
            //Suche Targetpostion am Bildschirm (HEIGHT 0-1, WIDTH 0-1 ist am Bildschirm)
            Vector2 viewPosition = mainCamera.WorldToViewportPoint(target.transform.position);

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
            if(distanceToCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = distanceToCenter.sqrMagnitude;
            }

        }

        if (closestTarget == null) { return false; }


        CurrentTarget = closestTarget;

        //Adde die Targets ind die CMTargetGroup (targets, Weight, Radius)
        cinemachineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }

    //Wenn man aus dem Targetmodus geht wird das CurrentTarget auf "null" gesetzt
    public void Cancel()
    {
        if (CurrentTarget == null) { return; }
      
        //Wenn man den TargetState verlässt wird das Target von der CM TargetGroup entfernt
        cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);

        CurrentTarget = null;
    }


    //Zerstörtes Target wird aus der Targeterliste und aus der Cinemachineliste entfernt
    private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target) 
        {
            //Entfernen aus CM TargetGroup Liste
            cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget=null;
        }

        //Unsubscribe 
        target.DestroyEvent -= RemoveTarget;
        //Entfernen aus targets liste
        targets.Remove(target);
    }
}
