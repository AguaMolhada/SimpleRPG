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
    public AudioMixer AudioMixer;
    public TMP_Dropdown ResolutionDropDown;
    public Slider ProgressSlider;
    public TMP_Text ProgressText;

    private Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        ResolutionDropDown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = "("+ resolutions[i].width + " x " + resolutions[i].height+")";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        ResolutionDropDown.AddOptions(options);
        ResolutionDropDown.value = currentResolutionIndex;
        ResolutionDropDown.RefreshShownValue();
    }

    public void PlayGame()
    {
        StartCoroutine("LoadAsynchronously");
    }

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

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetActiveMenu(GameObject x)
    {
        x.SetActive(!x.activeSelf);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int choosen)
    {
        QualitySettings.SetQualityLevel(choosen);
    }

    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("volume", volume);
    }

}
