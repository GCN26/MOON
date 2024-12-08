using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSaveSettings
{
    public static float masterVolume;
    public static float SFXVolume;
    public static float musicVolume;
    public static bool retroControl;
    public static int retroInt;
    //also include list of bools for completed levels

    public static void SetMasterVolume(float to)
    {
        masterVolume = to;
    }
    public static void SetSFXVolume(float to)
    {
        SFXVolume = to;
    }
    public static void SetMusicVolume(float to)
    {
        musicVolume = to;
    }
    public static void SetRetroControl(bool to)
    {
        retroControl = to;
    }
    public static void Save()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        if (retroControl == true)
        {
            PlayerPrefs.SetInt("RetroControl", 1);
        }
        else
        {
            PlayerPrefs.SetInt("RetroControl", 0);
        }
    }
    public static void Load()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        retroInt = PlayerPrefs.GetInt("RetroControl");
        if(retroInt == 0)
        {
            retroControl = false;
        }
        else if(retroInt == 1)
        {
            retroControl = true;
        }
    }
}