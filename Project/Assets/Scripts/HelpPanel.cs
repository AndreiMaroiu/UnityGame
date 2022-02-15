using UnityEngine;
using System.Collections;

public class HelpPanel : MonoBehaviour {

    public GameObject panel;
    public GameObject helpPanel;
    public GameObject button;

	void Start ()
    {
        button.SetActive(false);
        helpPanel.SetActive(false);
	}

	public void ChangePanels(bool value)
    {
        button.SetActive(value);
        helpPanel.SetActive(value);
        panel.SetActive(!value);
    }
}
