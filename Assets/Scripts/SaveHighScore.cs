using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHighScore : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;
    public float fastestTime = 0;

    //Declare new save
    const string newHighScore = "newHighScore";
    const string newFastestTime = "newFastestTime";

    bool gameSaved = false;

    void Start()
    {
        highScore = PlayerPrefs.GetInt(newHighScore, 0);
        fastestTime = PlayerPrefs.GetFloat(newFastestTime, 0f);
    }

 
    void Update()
    {
        //After gameover
        if (GameObject.FindGameObjectWithTag("Timer").GetComponent<UnityEngine.UI.Text>().text == "Mission Failed" ||
            GameObject.FindGameObjectWithTag("Timer").GetComponent<UnityEngine.UI.Text>().text == "Victory")
        {
            //Save game function
            if (!gameSaved)
            {
                SaveScore();
                gameSaved = true;
            }
        }
    }

    public void SaveScore()
    {

        //Get score
        int currentScore = int.Parse(GameObject.FindGameObjectWithTag("Score").GetComponent<UnityEngine.UI.Text>().text);

        //Save if the current score is higher than the old highscore
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt(newHighScore, currentScore);
            PlayerPrefs.Save();
            PlayerPrefs.SetFloat(newFastestTime, Time.timeSinceLevelLoad);
            PlayerPrefs.Save();
        }

        //Save the score if the current highscore is the same but the time is faster
        if (currentScore == highScore && Time.timeSinceLevelLoad < fastestTime)
        {
            PlayerPrefs.SetFloat(newFastestTime, Time.timeSinceLevelLoad);
            PlayerPrefs.Save();
        }
    }

    string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = String.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return timeText;
    }
}
