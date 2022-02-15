using UnityEngine;

public class NewHighScoreManager : MonoBehaviour {

    public GameObject panel;
    public GameObject highScorePanel;
    public GameObject image;

    void Start ()
    {

		if(PlayerPrefs.GetInt("NewhighScore") == 1)
        {
            image.SetActive(true);
            highScorePanel.SetActive(true);
            panel.SetActive(false);
        }
	}
	
}
