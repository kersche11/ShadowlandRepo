using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSzeneController : MonoBehaviour
{

    [SerializeField] GameObject production;
    [SerializeField] GameObject thanks;

    // Start is called before the first frame update
    void Start()
    {
        production.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        production.SetActive(true);
        thanks.SetActive(false);      
        StartCoroutine(Scene());
    }

    private IEnumerator Scene()
    {
        yield return new WaitForSeconds(8f);

        SceneManager.LoadScene("MainMenu");

    }
}
