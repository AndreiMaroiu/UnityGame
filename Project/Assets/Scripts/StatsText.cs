using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsText : MonoBehaviour {

    public Text[] text;

	void Start ()
    {
        text[0].text = Mathf.RoundToInt(Stats.money).ToString();
        text[1].text = Mathf.RoundToInt(Stats.averageMoney).ToString();
        text[2].text = Mathf.RoundToInt(Stats.games).ToString();
        text[3].text = Mathf.RoundToInt(Stats.skins).ToString();
        text[4].text = Mathf.RoundToInt(Stats.backgrounds).ToString();
        text[5].text = Mathf.RoundToInt(Stats.highscore).ToString();
        text[6].text = Mathf.RoundToInt(Stats.averageScore).ToString();
        text[7].text = Mathf.RoundToInt(Stats.powerups).ToString();
    }
}
