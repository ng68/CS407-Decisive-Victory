using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class Globals
{
    public static savedata[] saves = new savedata[3];
    public static int currLevel;
    public static string currUser;
    public static bool nonInitiated=true;
}
public class Init_LoadPreferences : MonoBehaviour
{
    #region Variables
    //BRIGHTNESS
    [Space(20)]
    [SerializeField] private Brightness brightnessEffect;
    [SerializeField] private Text brightnessText;
    [SerializeField] private Slider brightnessSlider;

    //VOLUME
    [Space(20)]
    [SerializeField] private Text volumeText;
    [SerializeField] private Slider volumeSlider;

    //SENSITIVITY
    [Space(20)]
    [SerializeField] private Text controllerText;
    [SerializeField] private Slider controllerSlider;

    //INVERT Y
    [Space(20)]
    [SerializeField] private Toggle invertYToggle;

    [Space(20)]
    [SerializeField] private bool canUse = false;
    [SerializeField] private bool isMenu = false;
    [SerializeField] private MenuController menuController;
    #endregion
    public SaveLoad saves;

    private void Awake()
    {
        if (Globals.nonInitiated)
        {
            Debug.Log("Loading player prefs test");
            for (int i = 0; i < 3; i++)
            {
                Globals.saves[i] = new savedata();
                Globals.saves[i].charname = "None";
            }
            Globals.saves[2].charname = "Luna";
            Globals.saves[2].scores[3] = 0;
            Globals.saves[2].levelswon = 0;
            Globals.saves[1].charname = "Willow";
            Globals.saves[1].levelswon = 1;
            Globals.saves[1].scores[3] = 4;
            Globals.currUser = "Kumquat";
            Globals.nonInitiated = false;
        }


        if (canUse)
        {
            //BRIGHTNESS
            if (brightnessEffect != null)
            {
                if (PlayerPrefs.HasKey("masterBrightness"))
                {
                    float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                    brightnessText.text = localBrightness.ToString("0.0");
                    brightnessSlider.value = localBrightness;
                    brightnessEffect.brightness = localBrightness;
                }

                else
                {
                    menuController.ResetButton("Brightness");
                }
            }

            //VOLUME
            if (PlayerPrefs.HasKey("masterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");

                volumeText.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                menuController.ResetButton("Audio");
            }

            //CONTROLLER SENSITIVITY
            if (PlayerPrefs.HasKey("masterSen"))
            {
                float localSensitivity = PlayerPrefs.GetFloat("masterSen");

                controllerText.text = localSensitivity.ToString("0");
                controllerSlider.value = localSensitivity;
                menuController.controlSenFloat = localSensitivity;
            }
            else
            {
                menuController.ResetButton("Graphics");
            }

            //INVERT Y
            if (PlayerPrefs.HasKey("masterInvertY"))
            {
                if (PlayerPrefs.GetInt("masterInvertY") == 1)
                {
                    invertYToggle.isOn = true;

                }

                else
                {
                    invertYToggle.isOn = false;
                }
            }
            if (PlayerPrefs.HasKey("activeProfile"))
            {
                Debug.LogWarning("PlayerPrefs entry found.");
                Debug.LogWarning(UnityEngine.Application.persistentDataPath);
                //saves.Load();
                Button thisButton = GameObject.Find("Profile_Button").GetComponent<Button>();
                string profName = PlayerPrefs.GetString("userName");
                int score = PlayerPrefs.GetInt("score");
                string toChange = "Welcome " + profName + "\nYour score is " + score;
                thisButton.GetComponentInChildren<Text>().text = toChange;
            }
            else
            {
                Debug.LogWarning("PlayerPrefs entry found.");
                Button thisButton = GameObject.Find("Profile_Button").GetComponent<Button>();
                string toChange = "Profile not loaded";
                thisButton.GetComponentInChildren<Text>().text = toChange;
            }
        }
    }
}
