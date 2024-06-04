using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject highScore;
    [SerializeField] private Button resume;
    [SerializeField] private Button toMenu;

    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        resume.gameObject.SetActive(true);
        toMenu.gameObject.SetActive(true);
        highScore.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Continue()
    {
        pausePanel.SetActive(false);
        resume.gameObject.SetActive(false);
        toMenu.gameObject.SetActive(false);
        highScore.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

}
