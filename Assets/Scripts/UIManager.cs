using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuPanel, gameOverPanel;

    public SliderValueToText maxStepSlider, fpsSlider, scaleSlider;
    [SerializeField] private Button startGame, resetData, replayLevel, goBack, readMore;
    [SerializeField] private Toggle numbersVToggle;

    [SerializeField] private ScalePreview scalePreview;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void EnableUI(bool enable)
    {
        mainMenuPanel.SetActive(enable);
        gameOverPanel.SetActive(!enable);

        if (enable)
        {
            OnResetData();
        }
    }

    void Start()
    {
        startGame.onClick.AddListener(OnStartGame);
        resetData.onClick.AddListener(OnResetData);
        replayLevel.onClick.AddListener(OnReplayLevel);
        goBack.onClick.AddListener(OnGoBack);
        readMore.onClick.AddListener(OnReadMore);
        numbersVToggle.onValueChanged.AddListener(OnToggleChange);
        EnableUI(true);
    }

    private void OnToggleChange(bool isOn)
    {
        scalePreview.EnableText(isOn);
    }

    private void OnStartGame()
    {
        EnableUI(false);
        gameOverPanel.SetActive(false);
        Manager.Instance.InitData((int)maxStepSlider.GetSliderValue(),(int)fpsSlider.GetSliderValue(),scaleSlider.GetSliderValue(), numbersVToggle.isOn);
        Manager.Instance.StartSpiral();
    }

    private void OnResetData()
    {
        maxStepSlider.SetSliderValue(5f);
        fpsSlider.SetSliderValue(60f);
        scaleSlider.SetSliderValue(1.5f);
        numbersVToggle.isOn = true;
    }

    private void OnReplayLevel()
    {
        Manager.Instance.ClearSpiral();
        gameOverPanel.SetActive(false);
        Manager.Instance.StartSpiral();
    }

    private void OnGoBack()
    {
        Manager.Instance.ClearSpiral();
        EnableUI(true);
    }

    private void OnReadMore()
    {
        Application.OpenURL("https://en.wikipedia.org/wiki/Ulam_spiral");
    }
}
