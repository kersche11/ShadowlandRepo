using CUAS.MMT;
using System.Collections;
using UnityEngine;

public class TreasureController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] GameObject SceneLoaderOpenWorld;
    [SerializeField] GameObject edelstein;

    private readonly int TreasureHash = Animator.StringToHash("OpenTreasure");
    private const float CrossFixedTimeDuration = 0.1f;
    private bool hasCollided = false;




    private void Start()
    {
        SceneLoaderOpenWorld.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCollided)
        {

            SoundManager.Instance.PlaySound(SoundManager.Sound.Kiste_erscheinen);
            animator.CrossFadeInFixedTime(TreasureHash, CrossFixedTimeDuration);
            hasCollided = true;
            StartCoroutine(Wait());
            SceneLoaderOpenWorld.SetActive(true);
            LevelManager.Instance.IncreaseDiamondCount();
            Debug.Log("DiamondCount: " + LevelManager.Instance.GetDiamondCount());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.Kiste_öffnen);

        edelstein.SetActive(false);
    }
}
