using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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
        currentScoreText.text = currentScore.ToString();
        highScoreText.text = highScore.ToString();

        if (currentScore >= 10)
        {
            this.SaveCurrentScore();
            SceneManager.LoadScene(3);
        }
    }

    public void SaveCurrentScore()
    {
        if (this.currentScore > this.highScore)
        {
            PlayerPrefs.SetInt("highScore", this.currentScore);
        }
    }
}
