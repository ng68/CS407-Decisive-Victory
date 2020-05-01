using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MenuController : MonoBehaviour
{
    int defer_update = 0;
    public string theName;
    #region Default Values
    [Header("Default Menu Values")]
    [SerializeField] private float defaultBrightness;
    [SerializeField] private float defaultVolume;
    [SerializeField] private int defaultSen;
    [SerializeField] private bool defaultInvertY;
    [SerializeField] public InputField inputField;


    [Header("Levels To Load")]
    public string level;
    private string levelToLoad;
    public string level2;
    public string level3;
    public string level4;
    public string level5;
    public string level6;
    public string level7;
    public string level8;
    public string tutorial;
    public string sandbox;


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
    [SerializeField] private GameObject InputCanvas;

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
    //public static int activeProfile;
    //public static savedata[] saves = new savedata[2];
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            int sum = 0;
            for (int j = 0; j < 10; j++)
            {
                //Debug.Log("Current value of J = "+j);
                sum = sum + Globals.saves[i].scores[j];
            }
            Globals.saves[i].sumofScores = sum;
        }
        menuNumber = 1;
        if (PlayerPrefs.HasKey("username"))
        {

        }
        else
        {
            //PlayerPrefs.SetString("userName", "Luna");
            //PlayerPrefs.SetInt("levels", 3);
        }

        Globals.currLevel = 11;
        Debug.Log("Main menu loaded, current level = " + Globals.currLevel);
        int currProfile = -1;
        for (int i = 0; i < 3; i++)
        {
            if (string.Equals(Globals.saves[i].charname, Globals.currUser))
            {
                currProfile = i;
            }
        }
        if (currProfile >= 0)
        {
            Debug.Log("Scores of current user " + Globals.currUser + ": " + Globals.saves[currProfile].scores[0] +
                " " + Globals.saves[currProfile].scores[1] + " " + Globals.saves[currProfile].scores[2] + " " + Globals.saves[currProfile].scores[3] + " "
                + Globals.saves[currProfile].scores[4] + " " + Globals.saves[currProfile].scores[5] + " " + Globals.saves[currProfile].scores[6] + " "
                + Globals.saves[currProfile].scores[7] + " " + Globals.saves[currProfile].scores[8] + " " + Globals.saves[currProfile].scores[9]);
                Button thisButton = GameObject.Find("Profile_Button").GetComponent<Button>();

            int score = Globals.saves[currProfile].sumofScores;
            string profName = Globals.saves[currProfile].charname;
            Debug.Log(Globals.saves[currProfile].sumofScores);
            Debug.Log(score);
            string toChange = "Welcome " + profName + "\nYour score is " + score;
            thisButton.GetComponentInChildren<Text>().text = toChange;
        }
        else
        {
            Debug.Log("User " + Globals.currUser + " not found");
            Button thisButton = GameObject.Find("Profile_Button").GetComponent<Button>();
            string toChange = "Profile Not Loaded";
            thisButton.GetComponentInChildren<Text>().text = toChange;
        }

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
        
        if (leaderboardCanvas.activeSelf&&defer_update==0)
        {
            for (int i = 0; i < 2; i++)
            {
                int sum = 0;
                for (int j = 0; j < 10; j++)
                {
                    //Debug.Log("Current value of J = "+j);
                    sum = sum + Globals.saves[i].scores[j];
                }
                Globals.saves[i].sumofScores = sum;
            }
            Array.Sort(Globals.saves);
            Button butt1 = GameObject.Find("Player_1").GetComponent<Button>();
            Button butt2 = GameObject.Find("Player_2").GetComponent<Button>();
            Button butt3 = GameObject.Find("Player_3").GetComponent<Button>();

            string b1Text = Globals.saves[2].charname + "               " + Globals.saves[2].sumofScores + "\n";
            string b2Text = Globals.saves[1].charname + "               " + Globals.saves[1].sumofScores + "\n";
            string b3Text = Globals.saves[0].charname + "               " + Globals.saves[0].sumofScores + "\n";

            butt1.GetComponentInChildren<Text>().text = b1Text;
            butt2.GetComponentInChildren<Text>().text = b2Text;
            butt3.GetComponentInChildren<Text>().text = b3Text;


        }
        if (menuDefaultCanvas.activeSelf)
        {
            int currProfile = -1;
            for (int i = 0; i < 3; i++)
            {
                if (string.Equals(Globals.saves[i].charname, Globals.currUser))
                {
                    currProfile = i;
                }
            }
            if (currProfile >= 0)
            {
                Button thisButton = GameObject.Find("Profile_Button").GetComponent<Button>();

                int score = Globals.saves[currProfile].sumofScores;
                string profName = Globals.saves[currProfile].charname;
                string toChange = "Welcome " + profName + "\nYour score is " + score;
                thisButton.GetComponentInChildren<Text>().text = toChange;
            }
            else
            {
                Button thisButton = GameObject.Find("Profile_Button").GetComponent<Button>();
                string toChange = "Profile Not Loaded";
                thisButton.GetComponentInChildren<Text>().text = toChange;
            }

        }

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
            for(int i = 0; i < 2; i++)
            {
                int sum = 0;
                for(int j = 0; j < 10; j++)
                {
                    sum = sum + Globals.saves[i].scores[j];
                }
                Globals.saves[i].sumofScores = sum;
            }
            Array.Sort(Globals.saves);

            Button butt1 = GameObject.Find("Player_1").GetComponent<Button>();
            Button butt2 = GameObject.Find("Player_2").GetComponent<Button>();

            string b1Text = Globals.saves[2].charname + "               " + Globals.saves[2].sumofScores + "\n";
            string b2Text = Globals.saves[1].charname + "               " + Globals.saves[1].sumofScores + "\n";

            butt1.GetComponentInChildren<Text>().text = b1Text;
            butt2.GetComponentInChildren<Text>().text = b2Text;

        }

        if (buttonType == "P1")
        {
            if (string.Equals(Globals.saves[2].charname, "None"))
            {
                leaderboardCanvas.SetActive(false);
                InputCanvas.SetActive(true);
                Globals.currSlot = 2;
            }
            else
            {
                PlayerPrefs.SetString("userName", Globals.saves[2].charname);
                PlayerPrefs.SetInt("score", Globals.saves[2].sumofScores);
                PlayerPrefs.SetInt("activeProfile", 2);
                Globals.currUser = Globals.saves[2].charname;
                Debug.Log("Current global user = " + Globals.currUser);
            }

            GoBackToMainMenu();
        }

        if (buttonType == "P2")
        {
            if (string.Equals(Globals.saves[1].charname, "None"))
            {
                leaderboardCanvas.SetActive(false);
                InputCanvas.SetActive(true);
                Globals.currSlot = 1;
            }
            else
            {
                PlayerPrefs.SetString("userName", Globals.saves[1].charname);
                PlayerPrefs.SetInt("score", Globals.saves[1].sumofScores);
                PlayerPrefs.SetInt("activeProfile", 1);
                Globals.currUser = Globals.saves[1].charname;
                Debug.Log("Current global user = " + Globals.currUser);
                GoBackToMainMenu();
            }

        }
        if (buttonType == "P3")
        {
            if (string.Equals(Globals.saves[0].charname, "None"))
            {
                leaderboardCanvas.SetActive(false);
                InputCanvas.SetActive(true);
                Globals.currSlot = 0;
            }
            else
            {
                PlayerPrefs.SetString("userName", Globals.saves[0].charname);
                PlayerPrefs.SetInt("score", Globals.saves[0].sumofScores);
                PlayerPrefs.SetInt("activeProfile", 0);
                Globals.currUser = Globals.saves[0].charname;
                Debug.Log("Current global user = " + Globals.currUser);
                GoBackToMainMenu();
            }

        }
        if (buttonType == "D1")
        {
            defer_update = 1;
            Globals.saves[2] = Globals.saves[1];
            Globals.saves[1] = Globals.saves[0];
            Globals.currUser = "None";
            Globals.saves[0] = new savedata();
            Globals.saves[0].charname = "None";
            Globals.saves[0].sumofScores = -1;
            for(int i = 0; i < 10; i++)
            {
                Globals.saves[0].scores[i] = 0;
            }
            
            Button butt1 = GameObject.Find("Player_1").GetComponent<Button>();
            Button butt2 = GameObject.Find("Player_2").GetComponent<Button>();
            Button butt3 = GameObject.Find("Player_3").GetComponent<Button>();

            string b1Text = Globals.saves[2].charname + "               " + Globals.saves[2].sumofScores + "\n";
            string b2Text = Globals.saves[1].charname + "               " + Globals.saves[1].sumofScores + "\n";
            string b3Text = Globals.saves[0].charname + "               " + Globals.saves[0].sumofScores + "\n";

            butt1.GetComponentInChildren<Text>().text = b1Text;
            butt2.GetComponentInChildren<Text>().text = b2Text;
            butt3.GetComponentInChildren<Text>().text = b3Text;
            defer_update = 0;
        }
        if (buttonType == "D2")
        {
            defer_update = 1;
            Globals.saves[1] = Globals.saves[0];
            Globals.saves[0] = new savedata();
            Globals.currUser = "None";
                Globals.saves[0].charname = "None";
                Globals.saves[0].sumofScores = -1;
                for (int i = 0; i < 10; i++)
                {
                    Globals.saves[0].scores[i] = 0;
                }
                Array.Sort(Globals.saves);
            Button butt1 = GameObject.Find("Player_1").GetComponent<Button>();
            Button butt2 = GameObject.Find("Player_2").GetComponent<Button>();
            Button butt3 = GameObject.Find("Player_3").GetComponent<Button>();

            string b1Text = Globals.saves[2].charname + "               " + Globals.saves[2].sumofScores + "\n";
            string b2Text = Globals.saves[1].charname + "               " + Globals.saves[1].sumofScores + "\n";
            string b3Text = Globals.saves[0].charname + "               " + Globals.saves[0].sumofScores + "\n";

            butt1.GetComponentInChildren<Text>().text = b1Text;
            butt2.GetComponentInChildren<Text>().text = b2Text;
            butt3.GetComponentInChildren<Text>().text = b3Text;
            defer_update = 0;
        }
        if (buttonType == "D3")
        {
            defer_update = 1;
            Globals.currUser = "None";
            Globals.saves[0] = new savedata();

            Globals.saves[0].charname = "None";
            Globals.saves[0].sumofScores = -1;
            for (int i = 0; i < 10; i++)
            {
                Globals.saves[0].scores[i] = 0;
            }
            Array.Sort(Globals.saves);
            Button butt1 = GameObject.Find("Player_1").GetComponent<Button>();
            Button butt2 = GameObject.Find("Player_2").GetComponent<Button>();
            Button butt3 = GameObject.Find("Player_3").GetComponent<Button>();

            string b1Text = Globals.saves[2].charname + "               " + Globals.saves[2].sumofScores + "\n";
            string b2Text = Globals.saves[1].charname + "               " + Globals.saves[1].sumofScores + "\n";
            string b3Text = Globals.saves[0].charname + "               " + Globals.saves[0].sumofScores + "\n";

            butt1.GetComponentInChildren<Text>().text = b1Text;
            butt2.GetComponentInChildren<Text>().text = b2Text;
            butt3.GetComponentInChildren<Text>().text = b3Text;
            defer_update = 0;
        }
        if (buttonType == "L1")
        {
            Globals.currLevel = 1;
            SceneManager.LoadScene(level);
        }
        if (buttonType == "L2")
        {
            Globals.currLevel = 2;
            SceneManager.LoadScene(level2);
        }
        if (buttonType == "L3")
        {
            Globals.currLevel = 3;
            SceneManager.LoadScene(level3);
        }
        if (buttonType == "L4")
        {
            Globals.currLevel = 4;
            SceneManager.LoadScene(level4);
        }
        if (buttonType == "L5")
        {
            Globals.currLevel = 5;
            SceneManager.LoadScene(level5);
        }
        if (buttonType == "L6")
        {
            Globals.currLevel = 6;
            SceneManager.LoadScene(level6);
        }
        if (buttonType == "L7")
        {
            Globals.currLevel = 7;
            SceneManager.LoadScene(level7);
        }
        if (buttonType == "L8")
        {
            Globals.currLevel = 8;
            SceneManager.LoadScene(level8);
        }
        if (buttonType == "SBX")
        {
            Globals.currLevel = 9;
            SceneManager.LoadScene(sandbox);
        }
        if (buttonType == "Tutorial")
        {
            Globals.currLevel = 0;
            SceneManager.LoadScene(tutorial);
        }
        if (buttonType == "InputText")
        {
            StoreName();
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
            Globals.currLevel = 0;
            SceneManager.LoadScene(tutorial);
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
        levelSelect.SetActive(false);
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
    public void StoreName()
    {
        string fromText = inputField.text;

        Globals.saves[Globals.currSlot].charname = fromText;
        
        //Debug.Log("Inputted name = " + theName);
        Globals.currUser = fromText;
        InputCanvas.SetActive(false);
        GoBackToMainMenu();
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
