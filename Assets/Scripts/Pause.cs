using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{

    [Header("Levels To Load")]
    public string level;
    private string levelToLoad;

    [SerializeField] private int menuNumber;

    #region Canvas
    [Header("Pause Canvas")]
    [Space(10)]
    [SerializeField] private GameObject PauseCanvas;
    [Space(10)]
    [Header("Pause Dialogs")]
    [SerializeField] private GameObject pauseDialog;
    [SerializeField] private GameObject startButton;
    #endregion

    #region Initialisation - Button Selection & Menu Order
    private void Start()
    {
        menuNumber = 1;
        if (PlayerPrefs.HasKey("username"))
        {

        }
        else
        {
            PlayerPrefs.SetString("userName", "Luna");
            PlayerPrefs.SetInt("levels", 3);
        }

    }
    #endregion

    //MAIN SECTION

    public void  MouseClick(string buttonType)
    {
        if (buttonType == "Pause")
        {   
            Time.timeScale = 0.0f;
            pauseDialog.SetActive(true);
        }
        if (buttonType == "Back")
        {
            if (!startButton.activeSelf)
                Time.timeScale = 1.0f;
            pauseDialog.SetActive(false);
        }
        if (buttonType == "Quit")
        {
            Time.timeScale = 1.0f;
            Debug.Log(level);
            SceneManager.LoadScene(level);
        }
        if (buttonType == "Start")
        {
            Time.timeScale = 1.0f;
            startButton.SetActive(false);
            
        }
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }
}


