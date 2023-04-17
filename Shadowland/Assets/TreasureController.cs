using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour
{

    [SerializeField] Animator animator;

    private readonly int TreasureHash = Animator.StringToHash("OpenTreasure");
    private const float CrossFixedTimeDuration = 0.1f;
    private bool hasCollided = false;


    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Player")&&!hasCollided)
        {
            animator.CrossFadeInFixedTime(TreasureHash, CrossFixedTimeDuration);
            hasCollided=true;
        }    
    }
}
