using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanels : MonoBehaviour {

    public GameObject[] panels;
    public GameObject[] lines;

	void Start ()
    {
        panels[0].SetActive(true);
        for(int i = 1; i < lines.Length; i++)
        {
            panels[i].SetActive(false);
            lines[i].SetActive(false);
        }
	}

	public void Switch(int panelNr)
    {
		for(int i = 0; i < panels.Length; i++)
        {
            if(i == panelNr)
            {
                panels[i].SetActive(true);
                lines[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
                lines[i].SetActive(false);
            }
        }
	}
}
