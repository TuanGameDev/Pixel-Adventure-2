using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController_UI: MonoBehaviour
{
    [SerializeField] private string mixerParmetr;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    [SerializeField] private float sliderMultiplier;

    public void SetupVolumeSlider()
    {
        slider.onValueChanged.AddListener(SliderValue);
        slider.minValue = .001f;
        slider.value = PlayerPrefs.GetFloat(mixerParmetr, slider.value);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(mixerParmetr, slider.value);
    }
    private void SliderValue(float value)
    {
        audioMixer.SetFloat(mixerParmetr, Mathf.Log10(value) * sliderMultiplier);
    }
}

