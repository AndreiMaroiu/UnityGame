using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static float score;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        score = 0;
    }

    void Update()
    {
        if (PlayerController.reviveCooldownFroScore == false)
        {
            score += Time.deltaTime;
        }
        text.text = "" + Mathf.RoundToInt(score);
    }

    public static void AddPoints(int pointsToAddToScore)
    {
        score += pointsToAddToScore;
    }

    void OnDisable()
    {
        if (score >= 0)
        {
            PlayerPrefs.SetInt("Score", (int)score);
        }
        else
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        if (PlayerPrefs.GetInt("Score") > GameManagerScript.highScore)
        {

            GameManagerScript.highScore = (int)score;
            GameManagerScript.Save();
            PlayerPrefs.SetInt("NewhighScore", 1);
        }
        else
        {
            PlayerPrefs.SetInt("NewhighScore", 0);
        }
    }
}

