using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScalePreview : MonoBehaviour
{
    [SerializeField] private Image nonPrime, prime;
    [SerializeField] private TextMeshProUGUI nonPrimeText, primeText;

    public Slider scaleSlider;
    
    void Start()
    {
        OnScaleChanged(scaleSlider.value);
        scaleSlider.onValueChanged.AddListener(OnScaleChanged);
    }

    private void OnScaleChanged(float value)
    {
        nonPrime.rectTransform.localScale = Vector3.one * (scaleSlider.maxValue - value);
        prime.rectTransform.localScale = Vector3.one * value;
    }

    public void EnableText(bool enable)
    {
        nonPrimeText.enabled = enable;
        primeText.enabled = enable;
    }
}
