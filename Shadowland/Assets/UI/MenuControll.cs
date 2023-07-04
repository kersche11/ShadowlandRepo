using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuControll : MonoBehaviour
{
    [SerializeField] Button? buttonStartGame;
    [SerializeField] Button? buttonMainMenu;
    [SerializeField] Button buttonExit;
    [SerializeField] Button buttonAbout;
    [SerializeField] Button buttonControls;
    [SerializeField] Button buttonBack;

    [SerializeField] GameObject about;
    [SerializeField] GameObject controls;
    [SerializeField] TriggerTutorial triggerTutorial;
    [SerializeField] GameObject menu;
    private bool isMenuOpen;



    // Start is called before the first frame update
    void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {

            isMenuOpen = false;
            SetFalse();
            OnClosed();
        }
        else
        {
            isMenuOpen =false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
     
            Time.timeScale = 1f;
            ActivateButton();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isMenuOpen == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                OnOpen();
                ActivateButton();
            }
            else if (isMenuOpen == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                OnClosed();
                
            }
        }
    }

    private void OnClosed()
    {
        SetFalse();

        //einfrieren
        Time.timeScale = 1f;
        isMenuOpen = false;


    }

    private void OnOpen()
    {
        menu.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            triggerTutorial.DisableTutorialImages();
        }
        
        //einfrieren
        Time.timeScale = 0f;
        isMenuOpen = true;
        ActivateButton();
        //buttonBack.gameObject.SetActive(true);

    }

    private void SetFalse()
    {
        menu.SetActive(false);
        about.SetActive(false);

        controls.SetActive(false);
        buttonBack.gameObject.SetActive(false);

    }
    public void ActivateButton()
    {
        buttonExit.onClick.AddListener(() =>
        {
            Debug.Log("Exit");
            Application.Quit();

        });
        buttonAbout.onClick.AddListener(() =>
        {
            about.SetActive(true);
            buttonBack.gameObject.SetActive(true);


        });

        buttonControls.onClick.AddListener(() =>
        {
            controls.SetActive(true);
            buttonBack.gameObject.SetActive(true);


        });
        buttonBack.onClick.AddListener(() =>
        {
            SetFalse();
            menu.SetActive(true);

        });


        buttonMainMenu?.onClick.AddListener(() =>
        {
            SetFalse();
            menu.SetActive(false);
            SceneManager.LoadScene("MainMenu");

        });


        buttonStartGame?.onClick.AddListener(() =>
        {
            SetFalse();
            menu.SetActive(false);
            LevelManager.Instance.ChangeLevel(1);
            SceneManager.LoadScene("OpenWorld");
            PlayerPrefs.SetInt(LevelManager.Instance.scorekey, 0);

        });


    }
}
