using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject OptionsPanel,CreditsPanel;
    public Toggle retro;
    public TextMeshProUGUI Controls;

    public void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        if (retro.isOn)
        {
            Controls.text = "Controls:\r\n\r\nM1/B - Fire\r\nWS/DPad UD/Left Stick UD - Move \r\nAD/DPad LR/Left Stick LR - Camera\r\n1,2,3/Bumpers - Swap Weapon\r\nEscape/Start - Pause";
        }
        else
        {
            Controls.text = "Controls:\r\n\r\nM1/B - Fire\r\nWASD/DPad/Left Stick - Move \r\nMouse/Right Stick - Camera\r\n1,2,3/Bumpers - Swap Weapon\r\nEscape/Start - Pause";
        }
    }
    public void LevelSelect()
    {
        //load level select panel
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }
    public void Options()
    {
        CreditsPanel.SetActive(false);
        if (OptionsPanel.activeSelf == true)
        {
            OptionsPanel.SetActive(false);
        }
        else
        {
            OptionsPanel.SetActive(true);
        }
    }
    public void Credits()
    {
        OptionsPanel.SetActive(false);
        if (CreditsPanel.activeSelf == true)
        {
            CreditsPanel.SetActive(false);
        }
        else
        {
            CreditsPanel.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
