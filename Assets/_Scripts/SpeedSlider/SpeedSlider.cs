using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    
    public static event Action<float> OnValueChanged;

    private void Start()
    {
        _slider.onValueChanged.AddListener((value) =>
        {
            OnValueChanged?.Invoke(value);
        });

        OnValueChanged?.Invoke(_slider.value);
    }
}
