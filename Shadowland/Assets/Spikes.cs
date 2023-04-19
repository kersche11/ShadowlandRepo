using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    private TrapDetector trapDetector;
    private Animator animator;
    GameObject Player;

    private readonly int TrapTriggerHash = Animator.StringToHash("TrapTrigger");
    private const float CrossFadeDuration = 0.1f;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("TrapDetector");
        trapDetector = Player.GetComponent<TrapDetector>();
        animator = GetComponent<Animator>();
        trapDetector.OnTrapDetect += OnTrap;
    }

    private void OnDestroy()
    {
        trapDetector.OnTrapDetect -= OnTrap;
    }

    private void OnTrap()
    {
        animator.CrossFadeInFixedTime(TrapTriggerHash, CrossFadeDuration);
    }
}

    
        

