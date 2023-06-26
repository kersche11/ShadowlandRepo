using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class MenuControll : MonoBehaviour
{
    [SerializeField] Button buttonExit;
    [SerializeField] Button buttonAbout;
    [SerializeField] Button buttonControls;    
    [SerializeField] Button buttonBack;

    [SerializeField] GameObject about;   
    [SerializeField] GameObject controls;

    [SerializeField] GameObject menu;
    private bool isMenuOpen;

   

    // Start is called before the first frame update
    void Start()
    {
      
        SetFalse();        
        isMenuOpen = false;       
       
    }

    // Update is called once per frame
    void Update()
    {        
          if (Input.GetKeyDown(KeyCode.Escape))
          {

            if(isMenuOpen == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;                
                OnOpen();                
            }
            else
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


    }
}
