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
    
    private int[] highScores = new int[5];
    
    private const string ScoreFileName = "./Assets/Scores/HighScores.txt";
    public TextMeshProUGUI highScoreBoard;
    public TextMeshProUGUI currentScore;
    
    public bool amIOpening;

    void Awake()
    {
        if (!File.Exists(ScoreFileName) && amIOpening)
        {
            File.WriteAllText(ScoreFileName, "0000");
            highScores[0] = 0;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ParseScoresFile();
        if (amIOpening)
        {
            UpdateMainBoard();
            GameOver.allEnemiesDead = false;
            GameOver.isPlayerDead = false;
            Time.timeScale = 1;
        }
        else
        {
            UpdateGameBoard(highScores[0]);
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
            StreamReader sr = new StreamReader(ScoreFileName);
            line = sr.ReadLine();
            int index = 0;
            while (line != null)
            {
                highScores[index] = int.Parse(line);
                line = sr.ReadLine();
                index++;
            }
            sr.Close();
            SortScores();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
    }

    private void UpdateMainBoard()
    {
        SortScores();
        StringBuilder sb = new StringBuilder("High Scores:\n");
        var index = 1;
        foreach (var score in highScores)
        {
            var tempScoreLength = 4 - score.ToString().Length;
            var scoreString = index.ToString() + " - ";
            for (var i = 0; i < tempScoreLength; i++)
            {
                scoreString += "0";
            }
            scoreString += score.ToString();
            sb.AppendLine(scoreString);
            index++;
        }
        highScoreBoard.SetText(sb);
    }

    private void UpdateGameBoard(int newScore)
    {
        var tempScoreLength = 4 - newScore.ToString().Length;
        var scoreString = "";
        for (var i = 0; i < tempScoreLength; i++)
        {
            scoreString += "0";
        }
        scoreString += newScore.ToString();
        highScoreBoard.SetText("High Score:\n" + scoreString);
    }

    private void UpdateHighScores(int newScore)
    {
        // TODO bug here, won't reorganize scores so oof
        SortScores();
        for (var index = 0; index < highScores.Length; index++)
        {
            if (highScores[index] < newScore)
            {
                var temp = highScores[index];
                highScores[index] = newScore;
                newScore = temp;
            }
        }
    }

    public void UpdateCurrentScore(int scoreIn)
    {
        var scoreTextCurrent = currentScore.GetParsedText();
        var scoreString = scoreTextCurrent.Replace("Score:\n", "");
        var scoreValue = int.Parse(scoreString) + scoreIn;
        var tempScoreLength = 4 - scoreValue.ToString().Length;
        scoreString = "";
        for (var i = 0; i < tempScoreLength; i++)
        {
            scoreString += "0";
        }

        UpdateHighScores(scoreValue);
        // UpdateGameBoard(scoreValue);

        scoreString += scoreValue.ToString();
        currentScore.SetText("Score:\n" + scoreString);
    }

    public void UpdateHighScoresFile()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var score in highScores)
        {
            sb.AppendLine(score.ToString());
        }
        File.WriteAllText(ScoreFileName, sb.ToString());
        ParseScoresFile();
        UpdateMainBoard();
    }
    
    private void SortScores()
    {
        Array.Sort(highScores);
        Array.Reverse(highScores);
    }
    
    
}
