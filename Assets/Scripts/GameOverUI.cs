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
            if (player.GetComponent<PlayerScript>().dead == true)
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
        SceneManager.LoadScene(currentSceneName);
    }
    public void MainMenu()
    {
        //send to main menu
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
