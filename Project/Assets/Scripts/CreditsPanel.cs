using System.ComponentModel;
using UnityEngine;

public class CreditsPanel : MonoBehaviour 
{
    public GameObject creditsPanel;
    public GameObject panel;
    public GameObject failedPanel;
    public GameObject statsPanel;

	private void Start ()
    {
        creditsPanel.SetActive(false);
        panel.SetActive(true);
        statsPanel.SetActive(false);
	}
	
	public void Switch(bool value)
    {
        creditsPanel.SetActive(value);
        panel.SetActive(!value);
    }

    public void Stats(bool value)
    {
        panel.SetActive(!value);
        statsPanel.SetActive(value);
    }

    public void OpenLink()
    {
        if (CheckInternet())
        {
            Application.OpenURL("https://www.youtube.com/watch?v=ER27jAZFQq8");
        }
        else
        {
            failedPanel.SetActive(true);
        }
    }

    private static bool CheckInternet()
    {
        //Error if return false
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    public void ClosePanel()
    {
        failedPanel.SetActive(false);
    }
}
