using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{

    public bool gameStarted;
    
    [Header("Levels To Load")]
    public string mainMenu;
    public string nextLevel;

    #region Canvas
    [Header("Pause Canvas")]
    [Space(10)]
    [SerializeField] private GameObject PauseCanvas;
    [Space(10)]
    [Header("Pause Dialogs")]
    [SerializeField] private GameObject pauseDialog;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject pauseButton;
    #endregion

    #region Canvas
    [Header("Game Log Canvas")]
    [Space(10)]
    [SerializeField] private GameObject LogText;
    #endregion

    #region Initialisation - Button Selection & Menu Order
    private void Start()
    {
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
            LogText.GetComponent<Text>().text += '\n' + "Game Paused!";
            pauseButton.SetActive(false);
            startButton.SetActive(false);
        }
        if (buttonType == "Back")
        {
            if (gameStarted == true)
                Time.timeScale = 1.0f;
            else
                startButton.SetActive(true);
            pauseDialog.SetActive(false);
            LogText.GetComponent<Text>().text += '\n' + "Game Resumed!";
            pauseButton.SetActive(true);
        }
        if (buttonType == "Quit")
        {
            Time.timeScale = 1.0f;
            Debug.Log(mainMenu);
            SceneManager.LoadScene(mainMenu);
        }
        if (buttonType == "Start")
        {
            Time.timeScale = 1.0f;
            startButton.SetActive(false);
            gameStarted = true;
            LogText.GetComponent<Text>().text += '\n' + "Game Started!";
        }
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }
}


