using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class Ads : MonoBehaviour 
{
    public int rewardMoney;
    public GameObject rewardPanel;
    public GameObject button;
    public GameObject failPanel;
    public Text text;

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {   
            if (CheckInternet())
            {
                button.SetActive(false);
                failPanel.SetActive(true);
                text.text = "Sorry! No more ads for now, try again later.";
            }
            else
            {
                button.SetActive(false);
                failPanel.SetActive(true);
                text.text = "Failed to show the ad. Check your internet connection and try again later.";
            }
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                GameManagerScript.playerMoney += rewardMoney;
                Stats.money += rewardMoney;
                rewardPanel.SetActive(true);
                button.SetActive(false);
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                button.SetActive(false);
                failPanel.SetActive(true);
                text.text = "Sorry but the ad was skipped.";
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                button.SetActive(false);
                failPanel.SetActive(true);
                text.text = "Sorry but the ad failed to be shown, try again later.";
                break;
        }
    }

    bool CheckInternet()
    {
        //return false if error
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}
