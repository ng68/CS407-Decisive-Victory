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
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject pauseButton;
    [Space(10)]
    [Header("Settings Dialogs")]
    [SerializeField] private GameObject settingsDialog;
    [SerializeField] private GameObject back2Button;
    #endregion

    #region Canvas
    [Header("Game Log Canvas")]
    [Space(10)]
    [SerializeField] private GameObject LogText;
    [SerializeField] private GameObject LogField;
    #endregion

    #region Canvas
    [Header("Description Canvas")]
    [Space(10)]
    [SerializeField] private GameObject DescText;
    [SerializeField] private GameObject DescField;
    #endregion

    #region Canvas
    [Header("Buy Canvas")]
    [Space(10)]
    [SerializeField] private GameObject Currency;
    #endregion

    #region Initialisation - Button Selection & Menu Order
    private void Start()
    {
        gameStarted = false;
        if (PlayerPrefs.HasKey("username"))
        {

        }
        else
        {

        }

    }
    #endregion

    //MAIN SECTION

    public void AppendLog(string message) {
        LogText.GetComponent<Text>().text += '\n' + message;
        Canvas.ForceUpdateCanvases();
        LogField.GetComponent<ScrollRect>().verticalNormalizedPosition = 0.0f;
        Canvas.ForceUpdateCanvases();
    }
    public void ChangeUnitDescription(string description) {
        DescText.GetComponent<Text>().text = "Unit Description:" + '\n' + '\n' + description;
        Canvas.ForceUpdateCanvases();
    }


    public void  MouseClick(string buttonType)
    {
        if (buttonType == "Pause")
        {   
            Time.timeScale = 0.0f;
            pauseDialog.SetActive(true);
            AppendLog("Game Paused!");
            pauseButton.SetActive(false);
            startButton.SetActive(false);
        }
        if (buttonType == "Back")
        {
            if (gameStarted == true)
                Time.timeScale = 1.0f;
            else if (gameStarted == false) {
                startButton.SetActive(true);
            }
            pauseDialog.SetActive(false);
            AppendLog("Game Resumed!");
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
            AppendLog("Game Started!");
        }

        if (buttonType == "Retry")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Pause Menu Game Settings.
        if (buttonType == "Settings")
        {
            // Enter Game Settings
            AppendLog("Settings");
            pauseDialog.SetActive(false);
            settingsDialog.SetActive(true);
        }
        if(buttonType == "Back2")
        {
            AppendLog("Back to Pause Menu!");
            settingsDialog.SetActive(false);
            pauseDialog.SetActive(true);
        }
        if(buttonType == "next")
        {
            Time.timeScale = 0.0f;
            Debug.Log(nextLevel);
            SceneManager.LoadScene(nextLevel);
        }
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public GameObject GetCurrency(){
    	return Currency;
    }

    public GameObject GetTextLog(){
        return LogText;
    }
}


