using UnityEngine;
public class ButtonHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Stone stone;
    [SerializeField] GameObject ElementToSetActive;
    [SerializeField] GameObject SceneLoader;


    private readonly int BtnDownHash = Animator.StringToHash("BtnDown");
    private const float CrossFadeDuration = 0.1f;



    private void Start()
    {
        SceneLoader.SetActive(false);
        ElementToSetActive.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other == stone.GetComponent<Collider>())
        {
            animator.Play("BtnDown");
            ElementToSetActive.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other == stone.GetComponent<Collider>())
        {
            animator.CrossFadeInFixedTime("BtnUp", CrossFadeDuration);
            ElementToSetActive.SetActive(false);
        }
    }

}
