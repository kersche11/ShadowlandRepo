using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Stone stone;
    [SerializeField] GameObject Element;
    
   
    private readonly int BtnDownHash = Animator.StringToHash("BtnDown");
    private const float CrossFadeDuration = 0.1f;



    private void Start()
    {
       
        Element.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
      if (other == stone.GetComponent<Collider>())
        {
            animator.Play("BtnDown");
            Element.SetActive(true);
        }
               
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == stone.GetComponent<Collider>())
        {
            animator.CrossFadeInFixedTime("BtnUp", CrossFadeDuration);
            Element.SetActive(false);
        }
    }

}
