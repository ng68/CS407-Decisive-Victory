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
            pauseDialog.SetActive(true);
        }
        if (buttonType == "Back")
        {
            pauseDialog.SetActive(false);
        }
        if (buttonType == "Quit")
        {
            Debug.Log(level);
            SceneManager.LoadScene(level);
        }
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }
}


