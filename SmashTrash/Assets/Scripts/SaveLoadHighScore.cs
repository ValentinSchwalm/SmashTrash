using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SaveLoadHighScore : MonoBehaviour

{
    public int currentScore;
    public int highScore;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = "Score: " + currentScore.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }
    }
}
