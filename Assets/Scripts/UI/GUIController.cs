﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GUIController.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Vianna 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once InconsistentNaming
public class GUIController : MonoBehaviour
{
    [Header("Global Settings")]
    public AudioMixer AudioMixer;
    public TMP_Dropdown ResolutionDropDown;
    public Slider VolumeSlider;
    private Resolution[] _resolutions;
    [Header("MainMenu Settings")]
    [Header("---------------------------------------------")]
    public Slider ProgressSlider;
    public TMP_Text ProgressText;


    private void Start()
    {
        _resolutions = Screen.resolutions;
        var volumeSliderValue = 0f;
        AudioMixer.GetFloat("volume", out volumeSliderValue);
        ResolutionDropDown.ClearOptions();
        VolumeSlider.value = volumeSliderValue;
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = "("+ _resolutions[i].width + " x " + _resolutions[i].height+")";
            options.Add(option);
            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        ResolutionDropDown.AddOptions(options);
        ResolutionDropDown.value = currentResolutionIndex;
        ResolutionDropDown.RefreshShownValue();
    }

    /// <summary>
    /// Start the game.
    /// </summary>
    public void PlayGame()
    {
        var temp = GameObject.FindObjectOfType<CharacterSelection>();
        var temp2 = GameObject.Find("SelectCharacter");
        var temp3 = GameObject.Find("Canvas").transform.GetChild(1).gameObject;

        if (temp.SelectCharacterSkin.Unlocked)
        {
            SetActiveMenu(temp2);
            SetActiveMenu(temp3);
            FindObjectOfType<CharacterSelection>().ClearAllChildern();
            StartCoroutine("LoadAsynchronously");
        }
        else
        {
            Debug.LogError("Se tem aidis?");
        }
    }
    /// <summary>
    /// Used in the loadscreen
    /// </summary>
    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        operation.allowSceneActivation = false;
        float progress = 0;
        while (operation.progress < 0.9f)
        {
            progress = .5f * operation.progress / 0.9f;
            ProgressSlider.value = progress;
            ProgressText.text = (progress * 100f).ToString("F0") + "%";
            yield return null;
        }
        while (progress < 1f)
        {
            progress = Mathf.Lerp(progress, 1f, 0.05f);
            if (progress > .99)
            {
                operation.allowSceneActivation = true;
            }
            ProgressSlider.value = progress;
            ProgressText.text = (progress* 100f).ToString("F0") + "%";
            yield return null;
        }

    }

    /// <summary>
    /// Close Game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Exit to main menu.
    /// </summary>
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);   
    }
    /// <summary>
    /// activate the menu
    /// </summary>
    /// <param name="x">GameObject to activate</param>
    public void SetActiveMenu(GameObject x)
    {
        x.SetActive(!x.activeSelf);
    }
    /// <summary>
    /// Set the game on fullscren or remove
    /// </summary>
    /// <param name="isFullscreen">Fullscreen(true)</param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    /// <summary>
    /// Set the game quality.
    /// </summary>
    /// <param name="choosen">Int for the quality.</param>
    public void SetQuality(int choosen)
    {
        QualitySettings.SetQualityLevel(choosen);
    }
    /// <summary>
    /// Set the game mixer volume.
    /// </summary>
    /// <param name="volume">Amount</param>
    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("volume", volume);
    }


}
