using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour {

    private int score;
    private int coins;

	void Start ()
    {
        Stats.games += 1;
        Stats.money += PlayerPrefs.GetInt("SessionMoney");
        Stats.highscore = GameManagerScript.highScore;
        score = PlayerPrefs.GetInt("Score");
        Stats.averageScore = AverageScore();

        coins = PlayerPrefs.GetInt("SessionMoney");
        Stats.averageMoney = AverageCoins();
        Stats.SaveStats();
	}

    float AverageScore()
    {
        float averageScore;

        averageScore = ((Stats.averageScore * (Stats.games - 1)) + score) / Stats.games;

        return averageScore ;
    }

    float AverageCoins()
    {
        float averageCoins;

        averageCoins = ((Stats.averageMoney * (Stats.games - 1)) + coins) / Stats.games;

        return  averageCoins;
    }
}
