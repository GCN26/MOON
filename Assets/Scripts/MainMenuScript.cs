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
    [TextArea(15, 20)]
    public string controlsTextRetro;
    [TextArea(15, 20)]
    public string controlsTextDefault;

    public void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        if (retro.isOn)
        {
            Controls.text = controlsTextRetro;
        }
        else
        {
            Controls.text = controlsTextDefault;
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
