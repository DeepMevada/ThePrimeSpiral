using System;
using TMPro;
using UnityEngine;

public class Number : MonoBehaviour
{
    private Transform _transform;
    
    [SerializeField] private SpriteRenderer sR;
    [SerializeField] private TMP_Text numText;

    [SerializeField] private float scalingFactor = 1.5f;

    private void Awake()
    {
        _transform = transform;
    }

    public void SetText(int number)
    {
        numText.text = (number).ToString();
    }

    public void SetNumbersVisible(bool visible)
    {
        numText.enabled = visible;
    }

    public void SetScaleFactor(float scaleF)
    {
        scalingFactor = scaleF;
    }
    
    public void ChangeForPrime(bool isPrime)
    {
        _transform.localScale = isPrime ? _transform.localScale * scalingFactor :_transform.localScale * (2 - scalingFactor);
    }
}
