using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetScore : MonoBehaviour
{
    private ScoreManager scoreManager;
    [SerializeField] ScoreManager scoreType;

    public void resetScore()
    {
        scoreType.ResetHighScore();
    }
}
