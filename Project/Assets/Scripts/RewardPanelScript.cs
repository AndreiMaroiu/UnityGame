using UnityEngine;
using System.Collections;

public class RewardPanelScript : MonoBehaviour {

    public GameObject panel;
    public GameObject button;
    public GameObject FailedAdPanel;

	void Start ()
    {
        panel.SetActive(false);
        FailedAdPanel.SetActive(false);
	}
	
	public void ClosePanel(bool setActive)
    {
        panel.SetActive(setActive);
        button.SetActive(true);
    }

    public void ClosedFailedPanel()
    {
        FailedAdPanel.SetActive(false);
        button.SetActive(true);
    }
}
