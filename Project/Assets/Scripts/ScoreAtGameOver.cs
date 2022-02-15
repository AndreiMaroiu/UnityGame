using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreAtGameOver : MonoBehaviour {

    int score;
    Text text;
    public Text highScore;

	void Start () {
        text = GetComponent<Text>();
        score = PlayerPrefs.GetInt("Score");
        text.text = "" + score;
        highScore.text = GameManagerScript.highScore.ToString();

	}

}
