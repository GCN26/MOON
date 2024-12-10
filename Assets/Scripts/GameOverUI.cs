using System;
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

    public bool victoryBool;
    public GameObject Victory;
    public TextMeshProUGUI timeTaken, enemiesSpared;

    public float timer;
    string currentSceneName;

    private void Start()
    {
        Victory.SetActive(false);
        currentSceneName = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if (player.GetComponent<PlayerScript>().victoryBool == true || (player.GetComponent<PlayerScript>().numberOfEnemies == 0 && player.GetComponent<PlayerScript>().gameTimer > 2f))
        {
            victoryBool = true;
        }
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

            if (victoryBool)
            {
                Victory.SetActive(true);
                var time = player.GetComponent<PlayerScript>().gameTimer;
                int minutes = (int)time / 60;
                int seconds = (int)time - 60 * minutes;
                timeTaken.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
                enemiesSpared.text = "Enemies Spared: " + player.GetComponent<PlayerScript>().numberOfEnemies;
                timeTaken.color = new Color(1, 1, 1, timer);
                enemiesSpared.color = new Color(1, 1, 1, timer);
            }
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
