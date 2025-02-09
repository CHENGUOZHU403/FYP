using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveUI : MonoBehaviour
{
    public GameObject saveUIPanel; 
    public Button startButton; 
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI playerMoneyText;
    public TextMeshProUGUI playerPositionText;
    public TextMeshProUGUI saveTimeText;
    
    public Button saveButton;
    public Button loadButton;
    public Button deleteButton;
    public Button closeButton;

    private string savePath;

    void Start()
    {
        if (saveUIPanel != null)
        {
            saveUIPanel.SetActive(false);
        }

        startButton.onClick.AddListener(OpenSaveUI);
        closeButton.onClick.AddListener(CloseSaveUI);

        savePath = Application.persistentDataPath + "/game_save.json";

        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
        deleteButton.onClick.AddListener(DeleteSave);

        UpdateSaveInfo();
    }

        void OpenSaveUI()
    {
        if (saveUIPanel != null)
        {
            saveUIPanel.SetActive(true); 
        }
    }

     void CloseSaveUI()
    {
        if (saveUIPanel != null)
        {
            saveUIPanel.SetActive(false);
        }
    }

    void UpdateSaveInfo()
    {
        if (File.Exists(savePath))
        {
            PlayerData playerData = SaveManager.Instance.playerData;

            playerNameText.text = $"PlayerName : {playerData.playerName}";
            playerLevelText.text = $"Level : Lv.{playerData.level}";
            playerMoneyText.text = $"Money : {playerData.money}G";
            playerPositionText.text = $"Player position : X:{playerData.positionX}, Y:{playerData.positionY}";
            saveTimeText.text = $"last save time : {System.DateTime.Now}"; 

            loadButton.interactable = true;
            deleteButton.interactable = true;
        }
        else
        {
            playerNameText.text = "PlayerName : None";
            playerLevelText.text = "Level : --";
            playerMoneyText.text = "Money : --";
            playerPositionText.text = "Player position : --";
            saveTimeText.text = "Last save time : None";

            loadButton.interactable = false;
            deleteButton.interactable = false;
        }
    }

    void SaveGame()
    {
        SaveManager.Instance.SaveGame();
        UpdateSaveInfo();
    }

    void LoadGame()
    {
        SaveManager.Instance.LoadGame();
        UpdateSaveInfo();
    }

    void DeleteSave()
    {
        SaveManager.Instance.ResetGame();
        UpdateSaveInfo();
    }

    
}
