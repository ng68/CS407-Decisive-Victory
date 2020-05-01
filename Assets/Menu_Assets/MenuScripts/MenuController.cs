using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MenuController : MonoBehaviour
{
    #region Default Values
    [Header("Default Menu Values")]
    [SerializeField] private float defaultBrightness;
    [SerializeField] private float defaultVolume;
    [SerializeField] private int defaultSen;
    [SerializeField] private bool defaultInvertY;

    [Header("Levels To Load")]
    public string level;
    private string levelToLoad;
    public string level2;


    [SerializeField] private int menuNumber;
    #endregion

    #region Menu Dialogs
    [Header("Main Menu Components")]
    [SerializeField] private GameObject menuDefaultCanvas;
    [SerializeField] private GameObject GeneralSettingsCanvas;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject gameplayMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject confirmationMenu;
    [SerializeField] private GameObject leaderboardCanvas;
    [SerializeField] private GameObject levelSelect;
    [Space(10)]
    [Header("Menu Popout Dialogs")]
    [SerializeField] private GameObject noSaveDialog;
    [SerializeField] private GameObject newGameDialog;
    [SerializeField] private GameObject loadGameDialog;
    [SerializeField] private GameObject aboutDialog;
    #endregion

    #region Slider Linking
    [Header("Menu Sliders")]
    [SerializeField] private Text controllerSenText;
    [SerializeField] private Slider controllerSenSlider;
    public float controlSenFloat = 2f;
    [Space(10)]
    [SerializeField] private Brightness brightnessEffect;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Text brightnessText;
    [Space(10)]
    [SerializeField] private Text volumeText;
    [SerializeField] private Slider volumeSlider;
    [Space(10)]
    [SerializeField] private Toggle invertYToggle;
    #endregion
    #region Initialisation - Button Selection & Menu Order
    public SaveLoad saver;
    public static int activeProfile;
    public static savedata[] saves = new savedata[2];
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
        for (int i = 0; i < 2; i++)
        {
            saves[i] = new savedata();
        }
        saves[0].charname = "Luna";
        saves[0].score = 4;
        saves[0].levelswon = 1;

        saves[1].charname = "Willow";
        saves[1].score = 0;
        saves[1].levelswon = 0;
        activeProfile = 0;
        /*
            Array.Sort(saves);


            Button butt1 = GameObject.Find("Player_1").GetComponent<Button>();
            Button butt2 = GameObject.Find("Player_2").GetComponent<Button>();

            string b1Text = saves[1].charname + "               " + saves[1].score + "\n";
            string b2Text = saves[0].charname + "               " + saves[1].score + "\n";

            butt1.GetComponentInChildren<Text>().text = b1Text;
            butt2.GetComponentInChildren<Text>().text = b2Text;
        */
    }
    #endregion

    //MAIN SECTION
    #region Confrimation Box
    public IEnumerator ConfirmationBox()
    {
        confirmationMenu.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationMenu.SetActive(false);
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuNumber == 2 || menuNumber == 7 || menuNumber == 8 || menuNumber == 9 || menuNumber == 10)
            {
                GoBackToMainMenu();
                ClickSound();
            }

            else if (menuNumber == 3 || menuNumber == 4 || menuNumber == 5)
            {
                GoBackToOptionsMenu();
                ClickSound();
            }

            else if (menuNumber == 6) //CONTROLS MENU
            {
                GoBackToGameplayMenu();
                ClickSound();
            }
        }
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    #region Menu Mouse Clicks
    public void MouseClick(string buttonType)
    {
        if (buttonType == "Controls")
        {
            gameplayMenu.SetActive(false);
            controlsMenu.SetActive(true);
            menuNumber = 6;
        }

        if (buttonType == "Graphics")
        {
            GeneralSettingsCanvas.SetActive(false);
            graphicsMenu.SetActive(true);
            menuNumber = 3;
        }

        if (buttonType == "Sound")
        {
            GeneralSettingsCanvas.SetActive(false);
            soundMenu.SetActive(true);
            menuNumber = 4;
        }

        if (buttonType == "Gameplay")
        {
            GeneralSettingsCanvas.SetActive(false);
            gameplayMenu.SetActive(true);
            menuNumber = 5;
        }

        if (buttonType == "Exit")
        {
            Debug.Log("YES QUIT!");
            Application.Quit();
        }

        if (buttonType == "Options")
        {
            menuDefaultCanvas.SetActive(false);
            GeneralSettingsCanvas.SetActive(true);
            menuNumber = 2;
        }

        if (buttonType == "LoadGame")
        {
            menuDefaultCanvas.SetActive(false);
            levelSelect.SetActive(true);
            menuNumber = 8;
        }

        if (buttonType == "NewGame")
        {
            menuDefaultCanvas.SetActive(false);
            newGameDialog.SetActive(true);
            menuNumber = 7;
        }
        if (buttonType == "About")
        {
            menuDefaultCanvas.SetActive(false);
            aboutDialog.SetActive(true);
            menuNumber = 9;
        }
        if (buttonType == "NewProfile")
        {

            menuDefaultCanvas.SetActive(false);
            leaderboardCanvas.SetActive(true);
            menuNumber = 10;
            Array.Sort(saves);

            Button butt1 = GameObject.Find("Player_1").GetComponent<Button>();
            Button butt2 = GameObject.Find("Player_2").GetComponent<Button>();

            string b1Text = saves[1].charname + "               " + saves[1].score + "\n";
            string b2Text = saves[0].charname + "               " + saves[0].score + "\n";

            butt1.GetComponentInChildren<Text>().text = b1Text;
            butt2.GetComponentInChildren<Text>().text = b2Text;

        }

        if (buttonType == "P1")
        {
            PlayerPrefs.SetString("userName", saves[1].charname);
            PlayerPrefs.SetInt("score", saves[1].score);
            PlayerPrefs.SetInt("activeProfile", 1);
            activeProfile = 1;
            GoBackToMainMenu();
        }

        if (buttonType == "P2")
        {
            PlayerPrefs.SetString("userName", saves[0].charname);
            PlayerPrefs.SetInt("score", saves[0].score);
            PlayerPrefs.SetInt("activeProfile", 0);
            activeProfile = 0;
            GoBackToMainMenu();
        }
        if (buttonType == "L1")
        {
            SceneManager.LoadScene(level);
        }
        if (buttonType == "L2")
        {
            SceneManager.LoadScene(level2);
        }
    }
    #endregion

    #region Volume Sliders Click
    public void VolumeSlider(float volume)
    {
        AudioListener.volume = volume;
        volumeText.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        Debug.Log(PlayerPrefs.GetFloat("masterVolume"));
        StartCoroutine(ConfirmationBox());
    }
    #endregion

    #region Brightness Sliders Click
    public void BrightnessSlider(float brightness)
    {
        brightnessEffect.brightness = brightness;
        brightnessText.text = brightness.ToString("0.0");
    }

    public void BrightnessApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", brightnessEffect.brightness);
        Debug.Log(PlayerPrefs.GetFloat("masterBrightness"));
        StartCoroutine(ConfirmationBox());
    }
    #endregion

    #region Controller Sensitivity
    public void ControllerSen()
    {
        controllerSenText.text = controllerSenSlider.value.ToString("0");
        controlSenFloat = controllerSenSlider.value;
    }
    #endregion

    public void GameplayApply()
    {
        #region Invert
        if (invertYToggle.isOn) //Invert Y ON
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            Debug.Log("Invert" + " " + PlayerPrefs.GetInt("masterInvertY"));
        }

        else if (!invertYToggle.isOn) //Invert Y OFF
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            Debug.Log(PlayerPrefs.GetInt("masterInvertY"));
        }
        #endregion

        #region Controller Sensitivity
        PlayerPrefs.SetFloat("masterSen", controlSenFloat);
        Debug.Log("Sensitivity" + " " + PlayerPrefs.GetFloat("masterSen"));
        #endregion

        StartCoroutine(ConfirmationBox());
    }

    #region ResetButton
    public void ResetButton(string GraphicsMenu)
    {
        if (GraphicsMenu == "Brightness")
        {
            brightnessEffect.brightness = defaultBrightness;
            brightnessSlider.value = defaultBrightness;
            brightnessText.text = defaultBrightness.ToString("0.0");
            BrightnessApply();
        }

        if (GraphicsMenu == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeText.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (GraphicsMenu == "Graphics")
        {
            controllerSenText.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            controlSenFloat = defaultSen;

            invertYToggle.isOn = false;

            GameplayApply();
        }
    }
    #endregion

    #region Dialog Options
    public void ClickNewGameDialog(string ButtonType)
    {
        if (ButtonType == "Yes")
        {
            SceneManager.LoadScene(level);
        }

        if (ButtonType == "No")
        {
            GoBackToMainMenu();
        }
    }

    public void ClickLoadGameDialog(string ButtonType)
    {
        if (ButtonType == "Yes")
        {
            if (PlayerPrefs.HasKey("SavedLevel"))
            {
                Debug.Log("I WANT TO LOAD THE SAVED GAME");
                //LOAD LAST SAVED SCENE
                levelToLoad = PlayerPrefs.GetString("SavedLevel");
                SceneManager.LoadScene(levelToLoad);
            }

            else
            {
                Debug.Log("Load Game Dialog");
                menuDefaultCanvas.SetActive(false);
                loadGameDialog.SetActive(false);
                noSaveDialog.SetActive(true);
            }
        }

        if (ButtonType == "No")
        {
            GoBackToMainMenu();
        }
    }
    public void ClickAboutDialog(string ButtonType)
    {
        if (ButtonType == "Back")
        {
            GoBackToMainMenu();
        }
    }
    #endregion

    #region Back to Menus
    public void GoBackToOptionsMenu()
    {
        GeneralSettingsCanvas.SetActive(true);

        graphicsMenu.SetActive(false);
        soundMenu.SetActive(false);
        gameplayMenu.SetActive(false);
        leaderboardCanvas.SetActive(false);
        //saver.saves = this.saves;
        //saver.Save();

        GameplayApply();
        BrightnessApply();
        VolumeApply();

        menuNumber = 2;
    }

    public void GoBackToMainMenu()
    {
        menuDefaultCanvas.SetActive(true);
        Button thisButton = GameObject.Find("Profile_Button").GetComponent<Button>();
        int score = saves[activeProfile].score;
        string profName = saves[activeProfile].charname;
        string toChange = "Welcome " + profName + "\nYour score is " + score;
        thisButton.GetComponentInChildren<Text>().text = toChange;
        leaderboardCanvas.SetActive(false);
        newGameDialog.SetActive(false);
        loadGameDialog.SetActive(false);
        aboutDialog.SetActive(false);
        noSaveDialog.SetActive(false);
        GeneralSettingsCanvas.SetActive(false);
        graphicsMenu.SetActive(false);
        soundMenu.SetActive(false);
        gameplayMenu.SetActive(false);
        menuNumber = 1;
    }

    public void GoBackToGameplayMenu()
    {
        controlsMenu.SetActive(false);
        gameplayMenu.SetActive(true);
        menuNumber = 5;
    }

    public void ClickQuitOptions()
    {
        GoBackToMainMenu();
    }

    public void ClickNoSaveDialog()
    {
        GoBackToMainMenu();
    }
    #endregion

    /*void OnEnable()
    {
        saves[activeProfile].score = PlayerPrefs.GetInt("score");
        Button thisButton = GameObject.Find("Profile_Button").GetComponent<Button>();
        string profName = saves[activeProfile].charname;
        int score = saves[activeProfile].score;
        string toChange = "Welcome " + profName + "\nYour score is " + score;
        thisButton.GetComponentInChildren<Text>().text = toChange;
    }
    */

}
