using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI valueText;

    public string format;

    private void Reset()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        OnSliderValueChanged(slider.value);
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        valueText.text = value.ToString(format);
    }

    public float GetSliderValue()
    {
        return slider.value;
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
    }
}
