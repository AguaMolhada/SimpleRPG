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
    public static bool IsPaused;
    public AudioMixer AudioMixer;
    public TMP_Dropdown ResolutionDropDown;
    public Slider ProgressSlider;
    public Slider VolumeSlider;
    public TMP_Text ProgressText;

    public GameObject PauseMenu;
    public GameObject InventoryMenu;

    private Resolution[] _resolutions;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.I) && !PauseMenu.activeSelf)
        {
            GameController.Instance.IsPaused = !GameController.Instance.IsPaused;
            SetActiveMenu(InventoryMenu);
        }
    }

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

    private void PauseGame()
    {
        PauseMenu.SetActive(true);
        GameController.Instance.IsPaused = true;
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        GameController.Instance.IsPaused = true;
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);   
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
