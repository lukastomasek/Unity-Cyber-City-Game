using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class SettingManager : MonoBehaviour
{
    

    public Toggle fullScreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown antialliasinDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown vSyncDropDown;
    public Slider audioSlider;
    public Button applyButton;
    public Resolution[] resolutions;


    public GameSettings gameSettings;

    // change it bg sound in game 
    public AudioSource music;

    public GameObject optionsPanel;

     void OnEnable()
     {
        gameSettings = new GameSettings();

        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle();  });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialliasinDropdown.onValueChanged.AddListener(delegate { OnAntiallaisingChange(); });
        vSyncDropDown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        audioSlider.onValueChanged.AddListener(delegate { OnAudioChange(); });
        applyButton.onClick.AddListener(delegate { ApplySettings(); });
        resolutions = Screen.resolutions;
        
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }


        LoadSettings();
     }

    public void OnFullScreenToggle()
    {
          gameSettings.fullScreen =  Screen.fullScreen = fullScreenToggle.isOn;
          gameSettings.resloutionIndex = resolutionDropdown.value;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height,
            Screen.fullScreen);
        // setting the right resolution and asign it to the game settings 
        gameSettings.resloutionIndex = resolutionDropdown.value;
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;     
    }

    public void OnAntiallaisingChange()
    {
        QualitySettings.antiAliasing = gameSettings.antialliasing = (int)Mathf.Pow(2f, antialliasinDropdown.value);
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = gameSettings.v_Sync = vSyncDropDown.value;
    }

    public void OnAudioChange()
    {
        music.volume = gameSettings.musicVolume = audioSlider.value;
    }

    public void ApplySettings()
    {
        SaveSettings();

        if (optionsPanel.activeInHierarchy)
            optionsPanel.SetActive(false);
    }

    public void SaveSettings()
    {
        // jsondata will contain serialized data which is game settings
        // true means that file will be formatted
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json",jsonData);
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText( Application.persistentDataPath + "/gamesettings.json"));

        // loading data from file 
        audioSlider.value = gameSettings.musicVolume;
        antialliasinDropdown.value = gameSettings.antialliasing;
        vSyncDropDown.value = gameSettings.v_Sync;
        textureQualityDropdown.value = gameSettings.textureQuality;
        resolutionDropdown.value = gameSettings.resloutionIndex;
        fullScreenToggle.isOn = gameSettings.fullScreen;
        Screen.fullScreen = gameSettings.fullScreen;

        resolutionDropdown.RefreshShownValue();
    }
   

} // end 
