using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public EquationManager equationManager;
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

    private void Awake()
    {
        equationManager = GetComponent<EquationManager>();
    }
    void Start()
    {
        equationManager.GenerateRandomEquation();
        score = 0;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
            {
                lineToMove++;
            }
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
            {
                lineToMove--;
            }
        }

        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded)
            {
                Jump();
            }
            anim.SetInteger("Stage", 1);
        }
        if (!controller.isGrounded)
        {
            anim.SetInteger("Stage", 2);
        }
        if (controller.isGrounded)
        {
            if (anim.GetInteger("Stage") == 2)
            {
                anim.SetInteger("Stage", 3);
            }
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
        if (transform.position == targetPosition)
        {
            return;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    }

    private void Jump()
    {
        dir.y = jumpForce;
    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir *  Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NonGate")
        {
            equationManager.GenerateRandomEquation();
        }
        else if (other.gameObject.tag == "CorrectGate")
        {
            if (equationManager.result == equationManager.currentAnswer)
            {
                score++;
                scoreText.text = score.ToString();
            }
            else
            {
                if (score > minScore)
                {
                    score--;
                    scoreText.text = score.ToString();
                }
                if (speed < maxSpeed)
                {
                    speed += 5;
                }
            }
            equationManager.GenerateRandomEquation();
        }
        else if (other.gameObject.tag == "UnCorrectGate")
        {
            if (equationManager.result == equationManager.currentAnswer)
            {
                if (score > minScore)
                {
                    score--;
                    scoreText.text = score.ToString();
                }
                if (speed < maxSpeed)
                {
                    speed += 5;
                }
            }
            else
            {
                score++;
                scoreText.text = score.ToString();
            }
            equationManager.GenerateRandomEquation();
        }
        if (highScore < score)
        {
            highScore = score;
        }
    }
}
