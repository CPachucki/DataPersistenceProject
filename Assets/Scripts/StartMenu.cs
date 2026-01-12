using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
using System.Diagnostics;

public class StartMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameEntry;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // Get player's name if it's been entered
        if (usernameEntry.text != null)
        {
            GameManager.Instance.Username = usernameEntry.text;
            UnityEngine.Debug.Log(GameManager.Instance.Username);
            
            // Load game scene
            SceneManager.LoadScene(1);
        }
        else
        {
            UnityEngine.Debug.Log("Enter a username");
        }          
    }
}
