// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InGameUIController.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    [Header("Menus")]
    public GameObject PauseMenu;
    public GameObject InventoryMenu;
    public GameObject InGameHud;
    /// <summary>
    /// Health slider.
    /// </summary>
    [Header("InGame")]
    public Slider HealthInGame;
    /// <summary>
    /// Experience Slider.
    /// </summary>
    public Slider ExpIngGame;
    /// <summary>
    /// Level text display.
    /// </summary>
    public TMP_Text LevelText;
    /// <summary>
    /// Gold text.
    /// </summary>
    [Header("Inventory")]
    [SerializeField] private TMP_Text _playerGold;
    /// <summary>
    /// Cash text.
    /// </summary>
    [SerializeField] private TMP_Text _playerCash;
    /// <summary>
    /// Nickname text.
    /// </summary>
    [SerializeField] private TMP_Text _playerNickname;
    /// <summary>
    /// Player selected class.
    /// </summary>
    [SerializeField] private TMP_Text _playerClass;
    /// <summary>
    /// Experience Slider.
    /// </summary>
    [SerializeField] private Slider _expSlider;
    /// <summary>
    /// Experience ammount in %
    /// </summary>
    [SerializeField] private TMP_Text _expPercent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.I) && !PauseMenu.activeSelf)
        {
            GameController.Instance.IsPaused = !GameController.Instance.IsPaused;
            InventoryMenu.SetActive(!InventoryMenu.activeSelf);
        }
        UpdateUiElemets();
    }

    /// <summary>
    /// Pause Game.
    /// </summary>
    private void PauseGame()
    {
        PauseMenu.SetActive(true);
        GameController.Instance.IsPaused = true;
        Time.timeScale = 0;
    }
    /// <summary>
    /// Resume Game.
    /// </summary>
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        GameController.Instance.IsPaused = true;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Update the ui elements.
    /// </summary>
    private void UpdateUiElemets()
    {
        var tempPlayer = GameController.Instance.Player;
        if (InventoryMenu.activeSelf)
        {
            _expSlider.value = Ultility.GetPercent(tempPlayer.PlayerStats.RequiredExperience, tempPlayer.PlayerStats.PlayerExperience);
            _expPercent.text = Ultility.GetPercentValue(tempPlayer.PlayerStats.RequiredExperience, tempPlayer.PlayerStats.PlayerExperience).ToString("#.##") + "%";
            _playerCash.text = tempPlayer.GoldAmount.ToString("##,###");
            _playerGold.text = tempPlayer.CashAmount.ToString("##,###");
            _playerNickname.text = tempPlayer.NickName + " - Lvl: " + tempPlayer.PlayerStats.PlayerLevel;
            _playerClass.text = tempPlayer.PlayerStats.PlayerClass.ToString();
        }
        else
        {
            HealthInGame.value = Ultility.GetPercent(tempPlayer.PlayerStats.Health, tempPlayer.PlayerStats.MaxHealth);
            ExpIngGame.value = Ultility.GetPercent(tempPlayer.PlayerStats.RequiredExperience, tempPlayer.PlayerStats.PlayerExperience);
            LevelText.text = tempPlayer.PlayerStats.PlayerLevel.ToString();
        }
    }

}
