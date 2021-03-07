using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public string sceneToLoad;
    private string m_AllHighScores = "High Scores:\n";
    private string scoreFileName = "./Assets/Scores/HighScores.txt";
    public TextMeshProUGUI highScoreBoard;
    public TextMeshProUGUI currentScore;
    public bool amIOpening;

    void Awake()
    {
        if (!File.Exists(scoreFileName))
        {
            File.WriteAllText(scoreFileName, "High Scores:\n");
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ParseScoresFile();
        if (amIOpening)
        {
            UpdateBoard();
        }
        else
        {
            UpdateMiniBoard(m_AllHighScores.Substring(13, 5));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && amIOpening)
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !amIOpening)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    
    private void ParseScoresFile()
    {
        String line;
        try
        {
            StreamReader sr = new StreamReader(scoreFileName);
            line = sr.ReadLine();
            // int index = 0;
            while (line != null)
            {
                m_AllHighScores += (line + "\n");
                // Debug.Log(line);
                // Debug.Log(highScoreArray[index]);
                line = sr.ReadLine();
            }

            sr.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
    }

    private void UpdateBoard()
    {
        // StringBuilder sb = new StringBuilder("High Scores:\n");
        highScoreBoard.SetText(m_AllHighScores);
    }

    private void UpdateMiniBoard(string newScore)
    {
        highScoreBoard.SetText("High Score:\n" + newScore);
    }

    private void UpdateHighScoreText(string newScore)
    {
        var oldHighScoreString = m_AllHighScores.Replace("High Score:\n", "");
        var newHighScoreString = "High Score:\n" + newScore.ToString() + "\n" + oldHighScoreString;
        m_AllHighScores = newHighScoreString;
        File.WriteAllText(scoreFileName, m_AllHighScores.Replace("High Score:\n", ""));
    }
    
    private void UpdateHighScores(int newScore)
    {
        var highScoreTextCurrent = highScoreBoard.GetParsedText();
        var highScoreString = highScoreTextCurrent.Replace("High Score:", "");
        var scoreValue = int.Parse(highScoreString);

        if (scoreValue >= newScore) return;
        
        var tempScoreLength = 4 - newScore.ToString().Length;
        highScoreString = "";
        for (var i = 0; i < tempScoreLength; i++)
        {
            highScoreString += "0";
        }
        
        highScoreString += newScore.ToString();
        UpdateMiniBoard(highScoreString);
        UpdateHighScoreText(highScoreString);
    }

    public void UpdateCurrentScore(int scoreIn)
    {
        var scoreTextCurrent = currentScore.GetParsedText();
        var scoreString = scoreTextCurrent.Replace("Score", "");
        var scoreValue = int.Parse(scoreString) + scoreIn; // error here on more than 1 iteration
        var tempScoreLength = 4 - scoreValue.ToString().Length;
        scoreString = "";
        for (var i = 0; i < tempScoreLength; i++)
        {
            scoreString += "0";
        }

        // UpdateHighScores(scoreValue);
        scoreString += scoreValue.ToString();
        currentScore.SetText("Score:\n" + scoreString);
    }
}
