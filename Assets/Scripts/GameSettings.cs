using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Slider livesSlider;
    [SerializeField] private Slider speedSlider;

    public int lives;
    public float speedFactor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lives = GameManager.Instance.Lives;
        livesSlider.value = lives;
        speedFactor = GameManager.Instance.BallSpeed;
        speedSlider.value = speedFactor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLives()
    {
        lives = (int)livesSlider.value;
        //GameManager.Instance.Lives = lives;
        GameManager.Instance.UpdateLives(lives);
    }

    public void SetSpeed()
    {
        speedFactor = (float)speedSlider.value;
        //GameManager.Instance.BallSpeed = speedFactor;
        GameManager.Instance.UpdateSpeed(speedFactor);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
