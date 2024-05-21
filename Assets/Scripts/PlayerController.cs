using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public EquationManager equationManager;
    public WordManager wordManager;
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] public static int score;
    [SerializeField] private Text scoreText;
    public static int highScore = 0;
    private Animator anim;
    private int minScore = 0;
    private int maxSpeed = 100;

    private int lineToMove = 1;
    public float lineDistance = 4;

    private bool isMathLevel;

    private void Awake()
    {
        equationManager = GetComponent<EquationManager>();
        wordManager = GetComponent<WordManager>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned in the inspector");
        }
        if (wordManager.wordText == null)
        {
            Debug.LogError("WordText is not assigned in the WordManager inspector");
        }
    }

    void Start()
    {
        isMathLevel = Random.Range(0, 2) == 0;
        if (isMathLevel)
        {
            equationManager.GenerateRandomEquation();
        }
        else
        {
            wordManager.GenerateRandomWord();
        }
        score = 0;
        UpdateScore();
    }

    private void Update()
    {
        if (SwipeController.swipeRight && lineToMove < 2)
        {
            lineToMove++;
        }

        if (SwipeController.swipeLeft && lineToMove > 0)
        {
            lineToMove--;
        }

        if (SwipeController.swipeUp && controller.isGrounded)
        {
            Jump();
            anim.SetInteger("Stage", 1);
        }
        if (!controller.isGrounded)
        {
            anim.SetInteger("Stage", 2);
        }
        if (controller.isGrounded && anim.GetInteger("Stage") == 2)
        {
            anim.SetInteger("Stage", 3);
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
        {
            targetPosition += Vector3.left * lineDistance / 2;
        }
        else if (lineToMove == 2)
        {
            targetPosition += Vector3.right * lineDistance / 2;
        }
        Vector3 moveDir = (targetPosition - transform.position).normalized * 25 * Time.deltaTime;
        controller.Move(moveDir.sqrMagnitude < (targetPosition - transform.position).sqrMagnitude ? moveDir : targetPosition - transform.position);
    }

    private void Jump()
    {
        dir.y = jumpForce;
    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMathLevel)
        {
            HandleMathLevel(other);
        }
        else
        {
            HandleWordLevel(other);
        }

        if (highScore < score)
        {
            highScore = score;
        }
    }

    private void HandleMathLevel(Collider other)
    {
        if (other.gameObject.CompareTag("NonGate"))
        {
            equationManager.GenerateRandomEquation();
        }
        else if (other.gameObject.CompareTag("CorrectGate"))
        {
            if (equationManager.result == equationManager.currentAnswer)
            {
                score++;
            }
            else
            {
                DecreaseScoreAndIncreaseSpeed();
            }
            equationManager.GenerateRandomEquation();
        }
        else if (other.gameObject.CompareTag("UnCorrectGate"))
        {
            if (equationManager.result != equationManager.currentAnswer)
            {
                score++;
            }
            else
            {
                DecreaseScoreAndIncreaseSpeed();
            }
            equationManager.GenerateRandomEquation();
        }
        UpdateScore();
    }

    private void HandleWordLevel(Collider other)
    {
        if (other.gameObject.CompareTag("NonGate"))
        {
            wordManager.GenerateRandomWord();
        }
        else if (other.gameObject.CompareTag("CorrectGate"))
        {
            if (wordManager.CheckWord(true))
            {
                score++;
            }
            else
            {
                DecreaseScoreAndIncreaseSpeed();
            }
            wordManager.GenerateRandomWord();
        }
        else if (other.gameObject.CompareTag("UnCorrectGate"))
        {
            if (wordManager.CheckWord(false))
            {
                score++;
            }
            else
            {
                DecreaseScoreAndIncreaseSpeed();
            }
            wordManager.GenerateRandomWord();
        }
        UpdateScore();
    }

    private void DecreaseScoreAndIncreaseSpeed()
    {
        if (score > minScore)
        {
            score--;
        }
        if (speed < maxSpeed)
        {
            speed += 5;
        }
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
}
