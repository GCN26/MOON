using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject OptionsPanel,CreditsPanel,LevelSelectPanel;
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
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        if (LevelSelectPanel.activeSelf == true)
        {
            LevelSelectPanel.SetActive(false);
        }
        else
        {
            LevelSelectPanel.SetActive(true);
        }
    }
    public void Options()
    {
        LevelSelectPanel.SetActive(false);
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
        LevelSelectPanel.SetActive(false);
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
    public void Level1Button()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Level2Button()
    {
        SceneManager.LoadScene("Level2");
    }
}
