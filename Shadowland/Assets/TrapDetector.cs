using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDetector : MonoBehaviour
{
    public event Action OnTrapDetect;
    private void OnTriggerEnter(Collider other)
    {
        OnTrapDetect?.Invoke();
    }
}
