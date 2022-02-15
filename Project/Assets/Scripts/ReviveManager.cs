using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class ReiveManager : MonoBehaviour
{
    public GameObject revivePanel;
    public GameObject PauseButton;
    public int cost;


    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                Time.timeScale = 1;
                revivePanel.SetActive(false);
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    public void Revive()
    {
        GameManagerScript.playerMoney -= cost;
        revivePanel.SetActive(false);
        Time.timeScale = 1;
    }

}
