using System;
using UnityEngine;

public class TrapDetector : MonoBehaviour
{
    public event Action OnTrapDetect;
    private void OnTriggerEnter(Collider other)
    {
        OnTrapDetect?.Invoke();
    }
}
