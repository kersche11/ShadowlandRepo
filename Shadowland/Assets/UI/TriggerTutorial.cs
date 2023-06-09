using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TriggerTutorial : MonoBehaviour
{

    [SerializeField] GameObject attackImage;
    [SerializeField] GameObject jumpImage;
    [SerializeField] GameObject dodgeImage;
    [SerializeField] GameObject stoneImage;
    [SerializeField] GameObject bossImage;
    [SerializeField] VideoPlayer video;




    // Start is called before the first frame update
    void Start()
    {
        DisableTutorialImages();
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("AttackColliderT"))
        {
            attackImage.SetActive(true);
            StartCoroutine(Einblenden());

        }
        if (other.CompareTag("JumpColliderT"))
        {
            jumpImage.SetActive(true);
            StartCoroutine(Einblenden());

        }
        if (other.CompareTag("StoneColliderT"))
        {
            stoneImage.SetActive(true);
            StartCoroutine(Einblenden());

        }
        if (other.CompareTag("DodgeColliderT"))
        {
            dodgeImage.SetActive(true);
            StartCoroutine(Einblenden());

        }
        if (other.CompareTag("BossColliderT"))
        {
            bossImage.SetActive(true);
            StartCoroutine(Einblenden());
        }

    }

    private IEnumerator Einblenden()
    {
        yield return new WaitForSeconds(5);
        attackImage.SetActive(false);
        jumpImage.SetActive(false);
        stoneImage.SetActive(false);
        dodgeImage.SetActive(false);
        bossImage.SetActive(false);
    }

    public void DisableTutorialImages()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            attackImage.SetActive(false);
            jumpImage.SetActive(false);
            dodgeImage.SetActive(false);
            stoneImage.SetActive(false);
            bossImage.SetActive(false);
        }
        
    }
}
