using UnityEngine;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public String Username;
    public int HighScore = 0;
    public String HighScoreUsername;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    class SaveData
    {
        public String username;
        public int highScore;
    }

    public void SaveHighScore(int score)
    {
        // High score = current score and high score name = current name
        SaveData data = new SaveData();
        data.username = Username;
        data.highScore = score;
        // Save new high score data to JSON file
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        
        UnityEngine.Debug.Log(data.username + ", " + data.highScore);
    }

    public void LoadHighScore()
    {
        // Load high score data from JSON file
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScoreUsername = data.username;
            HighScore = data.highScore;
        }
    }
}
