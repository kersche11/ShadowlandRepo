using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] GameObject Righthand;

    private void Start()
    {
        transform.position = Righthand.transform.position;

    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Righthand.transform.position;
            transform.rotation = Righthand.transform.rotation;
            
        
    }
}
