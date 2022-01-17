using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] string volumeParamter;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    float multiplier = 30;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParamter, slider.value);
    }

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSlider);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParamter, slider.value);
    }
    private void HandleSlider(float value)
    {
        mixer.SetFloat(volumeParamter, Mathf.Log10(value) * multiplier);
    }
}
