using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public GameObject ballPrefab; 
    private GameObject ball;
    private Rigidbody rbBall;
    public Transform paddle;   
    private Transform paddleParent;  

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private float ballSpeed = 2.0f;
    private float speedFactor = 1.0f;
    private int lives = 1;

    private bool m_Started = false;
    private int m_Points;
    private int highScore;
    private String username;
    private String highScoreUsername;
    
    private bool m_GameOver = false;

    void Awake()
    {
        username = GameManager.Instance.Username;
        UpdateScoreText();

        highScoreUsername = GameManager.Instance.HighScoreUsername;
        highScore = GameManager.Instance.HighScore;
        HighScoreText.text = $"High Score: {highScore}, {highScoreUsername}";

        speedFactor = GameManager.Instance.BallSpeed;

        lives = GameManager.Instance.Lives;

        ball = Instantiate(ballPrefab);
        ball.transform.position = paddle.transform.position;
        paddleParent = ball.transform.parent;
        rbBall = ball.GetComponent<Rigidbody>();

    }

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            // Start game
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();


                ball.transform.SetParent(null);
                
                rbBall.AddForce(forceDir * ballSpeed * speedFactor, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            // Restart game
            if (Input.GetKeyDown(KeyCode.Space))
            {

                GameManager.Instance.LoadGameData();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            } 
            // Return to title
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        UpdateScoreText();
    }

    public void SubtractLife()
    {
        lives--;
        UpdateScoreText();
        if (lives <= 0)
        {
            GameOver();
        } 
        else
        {
            // Reset ball over paddle
            rbBall.linearVelocity = Vector3.zero;
            //Ball.angularVelocity = Vector3.zero;
            rbBall.Sleep();
            ball.GetComponent<Ball>().velocity = Vector3.zero;
            m_Started = false;
            ball.transform.position = paddleParent.position;
            ball.transform.SetParent(paddleParent);   
        }
    }

    public void UpdateScoreText()
    {
        ScoreText.text = $"{username}'s Score: {m_Points}, Lives: {lives}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // Call SaveHighScore method from GameManager singleton
        if (m_Points > highScore)
        {
            GameManager.Instance.UpdateHighScore(m_Points);
        }

    }

    public bool ReturnGameOver()
    {
        return m_GameOver;
    }
}
