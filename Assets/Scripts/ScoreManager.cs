using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private PlayerController playerCont;
    [SerializeField] Text highScoreText;

    public int highScore;

    private void Update()
    {
        highScore = PlayerController.score;
        if (PlayerPrefs.GetInt("score") <= highScore)
        {
            PlayerPrefs.SetInt("score", highScore);
        }
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("score").ToString();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("score", 0);
        highScore = 0;
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("score").ToString();
    }
}
