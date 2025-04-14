using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject objectivePanel;
    [SerializeField] private TMP_Text objectiveText;
    // [SerializeField] private Slider progressSlider;
    // [SerializeField] private TMP_Text waveCounter;

    [Header("Settings")]
    [SerializeField] private int totalWaves = 3;
    // [SerializeField] private float autoShowDelay = 0.5f;
    private int currentWavesCleared = 0;

    void Start()
    {
        objectivePanel.SetActive(false);
        UpdateObjectiveDisplay();
    }


    public void OnWaveCleared()
    {
        currentWavesCleared++;
        UpdateObjectiveDisplay();
        
        // if(currentWavesCleared >= totalWaves)
        // {
        //     
        //     FindObjectOfType<DoorController>().UnlockBossDoor();
        // }
    }

    private void UpdateObjectiveDisplay()
    {
        objectiveText.text = $"Defeat {totalWaves} wave enemies and solve 3 questions to unlock the boss room";
        // progressSlider.value = (float)currentWavesCleared / totalWaves;
        // waveCounter.text = $"{currentWavesCleared}/{totalWaves}";
    }

    public void ToggleObjectivePanel(bool show)
    {

        objectivePanel.SetActive(show);
    }
}