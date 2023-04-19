using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private Animator spikeAnimator;
    private readonly int TrapTriggerHash = Animator.StringToHash("TrapTrigger");
    private const float CrossFadeDuration = 0.1f;
    private void OnTriggerEnter(Collider other)
   {
        if (!other.GetComponent<TrapDetector>()) { return; }
      
    spikeAnimator.CrossFadeInFixedTime(TrapTriggerHash, CrossFadeDuration);
}
}
