using UnityEngine;
using System;

using System.IO;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public String Username;

    public int HighScore = 0;
    public String HighScoreUsername;
    public float BallSpeed = 1;
    public int Lives = 1;

    [System.Serializable]
    class GameData
    {
        public String username;
        public int highScore;
        public float ballSpeed;
        public int lives;
    }
    private GameData data = new GameData();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();
        UnityEngine.Debug.Log("High Score: " + HighScore + ", " + HighScoreUsername);
        UnityEngine.Debug.Log("Lives: " + Lives);
        UnityEngine.Debug.Log("Speed: " + BallSpeed);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }    

    private void SaveGameData(GameData data)
    {
        // Save new high score data to JSON file
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);        
    }

    public void UpdateHighScore(int score)
    {
        // High score = current score and high score name = current name
        //GameData data = LoadGameData();
        data.username = Username;
        data.highScore = score;
        SaveGameData(data);
        //UnityEngine.Debug.Log(data.username + ", " + data.highScore);
    }

    public void UpdateLives(int lives)
    {
        Lives = lives;
        data.lives = lives;
        SaveGameData(data);
    }

    public void UpdateSpeed(float speed)
    {
        BallSpeed = speed;
        data.ballSpeed = speed;
        SaveGameData(data);
    }

    public void LoadGameData()
    {
        // Load high score data from JSON file
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);

            HighScoreUsername = data.username;
            HighScore = data.highScore;
            Lives = data.lives;
            BallSpeed = data.ballSpeed;
        }
        UnityEngine.Debug.Log("Data loaded.");
    }
}
