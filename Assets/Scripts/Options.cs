using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider SFXSlider;
    public Slider MusicSlider;
    public Toggle RetroControlToggle;

    private void Start()
    {
        PlayerSaveSettings.Load();
        UpdateSliders();
    }
    public void SetMasterVolume(float value)
    {
        PlayerSaveSettings.SetMasterVolume(value);
    }
    public void SetSFXVolume(float value)
    {
        PlayerSaveSettings.SetSFXVolume(value);
    }
    public void SetMusicVolume(float value)
    {
        PlayerSaveSettings.SetMusicVolume(value);
    }
    public void SetRetroControl(bool value)
    {
        PlayerSaveSettings.SetRetroControl(value);
    }
    public void UpdateSliders()
    {
        MasterSlider.normalizedValue = PlayerSaveSettings.masterVolume;
        SFXSlider.normalizedValue = PlayerSaveSettings.SFXVolume;
        MusicSlider.normalizedValue = PlayerSaveSettings.musicVolume;
        RetroControlToggle.isOn = PlayerSaveSettings.retroControl;
    }

    public void SaveOptions()
    {
        PlayerSaveSettings.Save();
    }
}
