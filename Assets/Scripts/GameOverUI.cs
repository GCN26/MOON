using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public GameObject player, GOUI;
    public TextMeshProUGUI GOText,restartbtext,menubtext,quitbtext;
    public Button restartb, menub, quitb;
    public GameObject optionsPanel;

    public float timer;
    string currentSceneName;

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        
        if (timer < 1.1f)
        {
            if (player.GetComponent<PlayerScript>().dead == true|| player.GetComponent<PlayerScript>().victoryBool == true || (player.GetComponent<PlayerScript>().numberOfEnemies == 0 && player.GetComponent<PlayerScript>().gameTimer > 2f))
            {
                timer += Time.deltaTime;
                GOUI.SetActive(true);
            }
            GOText.color = new Color(1, 1, 1, timer);
            restartb.GetComponent<Image>().color = new Color(1, 1, 1, timer);
            restartbtext.color = new Color(0, 0, 0, timer);
            menub.GetComponent<Image>().color = new Color(1, 1, 1, timer);
            menubtext.color = new Color(0, 0, 0, timer);
            quitb.GetComponent<Image>().color = new Color(1, 1, 1, timer);
            quitbtext.color = new Color(0, 0, 0, timer);
        }
        else
        {
            restartb.GetComponent<Button>().interactable = true;
            menub.GetComponent<Button>().interactable = true;
            quitb.GetComponent<Button>().interactable = true;
        }
    }
    public void RestartLevel()
    {
        optionsPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneName);
    }
    public void MainMenu()
    {
        optionsPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.None;
    }
    public void QuitGame()
    {
        optionsPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Application.Quit();
    }
    public void ResumeBut()
    {
        optionsPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void OptionsBut()
    {
        if (optionsPanel.activeSelf == false)
        {
            optionsPanel.SetActive(true);
        }
        else optionsPanel.SetActive(false);
    }
}
