using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpShop : MonoBehaviour 
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Slider[] slider;
    [SerializeField] private Text text;
    [SerializeField] private PowerUpObject[] powerUps;

    private int number;

	void Start ()
    {
        panel.SetActive(false);
        for(int i = 0; i < slider.Length; i++)
        {
            slider[i].value = 15 + 25 * (SavePowerUps.lvl[i] - 1);
        }
	}

    public void SetPowerUp(int value)
    {
        number = value;
    }

    public void ConfirmPanel(bool value)
    {
        panel.SetActive(value);

        if (SavePowerUps.lvl[number] == 0)
        {
            text.text = $"Are you sure you want to unlock The {powerUps[number]} for {SavePowerUps.price[number]}";
        }
        else
        {
            int price = SavePowerUps.price[number] / (SavePowerUps.lvl[number] - 1) + SavePowerUps.price[number] * SavePowerUps.lvl[number];
            text.text = $"Are you sure you want to upgrade The{powerUps[number]} for {price}";
        }
    }

    public void BuyPowerUp()
    {
        SavePowerUps.lvl[number] += 1;
        SavePowerUps.price[number] = SavePowerUps.price[number]/(SavePowerUps.lvl[number] -1) + SavePowerUps.price[number] * SavePowerUps.lvl[number];
        slider[number].value = 15 + 25 * (SavePowerUps.lvl[number] -1);
    }
}
