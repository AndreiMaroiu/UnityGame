using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class BackgroundManager : MonoBehaviour 
{
    public Sprite[] sprites;
    public string[] nameOfColor;
    public GameObject background;
    public Image color;
    public Text text;
    public GameObject failedPanel;
    public Text failedText;
    public GameObject buyButton;
    public int colorPrice;
    public GameObject[] fade;
    public GameObject diceCost;
    public GameObject quad;
    public Material[] materials;
    public GameObject confirmPanel;
    public Text confirmText;
    public GameObject diceFade;

    private string confirmString;
    private int colorNumber;
    private int currentColor;

    private List<BgList> backgroundsList = new List<BgList>();
    private bool[] availability;
    private int count;
    private bool diceAvailability = false;

	private void Start ()
    {
        availability = new bool[sprites.Length];
        Load();
        BuildBGData();
        Fade();
        colorNumber = GameManagerScript.bgColor;
        color.sprite = sprites[colorNumber];
        text.text = nameOfColor[colorNumber];
        buyButton.SetActive(false);
        Debug.Log(GameManagerScript.randomColor.ToString());
        
        if (diceAvailability)
        {
            diceCost.SetActive(false);
        }
        
        confirmPanel.SetActive(false);
        diceFade.SetActive(!diceAvailability);
	}

    private void Fade()
    {
        for(int i = 0; i < fade.Length; ++i)
        {
            fade[i].SetActive(!backgroundsList[i + 1].Availability);
        }
    }

    public void Preview(int previewNumber)
    {
        if(!backgroundsList[previewNumber].Availability)
        {
            quad.GetComponent<MeshRenderer>().material = materials[previewNumber];
            currentColor = previewNumber;
            color.sprite = sprites[previewNumber];
            text.text = nameOfColor[previewNumber];
            buyButton.SetActive(true);
        }
        else
        {
            quad.GetComponent<MeshRenderer>().material = materials[previewNumber];
            currentColor = previewNumber;
            color.sprite = sprites[previewNumber];
            text.text = nameOfColor[previewNumber];
            buyButton.SetActive(false);
            GameManagerScript.bgColor = previewNumber;
            GameManagerScript.randomColor = false;
        }
    }

	public void BuyColor()
    {
	    if(GameManagerScript.playerMoney >= colorPrice)
        {
            GameManagerScript.bgColor = currentColor;
            GameManagerScript.playerMoney -= colorPrice;
            GameManagerScript.randomColor = false;
            Save();
            fade[currentColor -1].SetActive(false);
            backgroundsList[currentColor].Availability = true;
            buyButton.SetActive(false);
            Stats.backgrounds += 1;
            Stats.SaveStats();
        }
        else
        {
            failedPanel.SetActive(true);
            failedText.text = "You don't have enough money!";
        }
	}

    public void RandomBackground()
    {
        if((GameManagerScript.playerMoney >= (colorPrice * 2)) && (!diceAvailability))
        {
            int randomNumber = UnityEngine.Random.Range(0, sprites.GetLength(0));
            while (randomNumber == GameManagerScript.bgColor)
            {
                randomNumber = UnityEngine.Random.Range(0, sprites.GetLength(0));
            }
            GameManagerScript.playerMoney -= 1000;
            GameManagerScript.randomColor = true;
            quad.GetComponent<MeshRenderer>().material = materials[randomNumber];
            diceAvailability = true;
            Save();
            color.sprite = sprites[randomNumber];
            text.text = nameOfColor[randomNumber];
            Save();
            GameManagerScript.SaveBackground();
            GameManagerScript.Save();
            diceCost.SetActive(false);
            diceFade.SetActive(false);
        }
        else if (diceAvailability)
        {
            int randomNumber = UnityEngine.Random.Range(0, sprites.GetLength(0));
            
            while (randomNumber == GameManagerScript.bgColor)
            {
                randomNumber = UnityEngine.Random.Range(0, sprites.GetLength(0));
            }

            GameManagerScript.randomColor = true;
            diceAvailability = true;
            quad.GetComponent<MeshRenderer>().material = materials[randomNumber];
            color.sprite = sprites[randomNumber];
            text.text = nameOfColor[randomNumber];
            Save();
            GameManagerScript.Save();
            GameManagerScript.SaveBackground();
        }
        
        Save();
    }

    public void Dice()
    {
        if(GameManagerScript.playerMoney >= colorPrice)
        {
            int randomNumber = UnityEngine.Random.Range(0, sprites.GetLength(0));
            
            while(randomNumber == GameManagerScript.bgColor)
            {
                randomNumber = UnityEngine.Random.Range(0, sprites.GetLength(0));
            }
            
            quad.GetComponent<MeshRenderer>().material = materials[randomNumber];
            GameManagerScript.playerMoney -= 500;
            GameManagerScript.randomColor = false;
            GameManagerScript.bgColor = randomNumber;
            color.sprite = sprites[randomNumber];
            text.text = nameOfColor[randomNumber];
        }
        else
        {
            failedPanel.SetActive(true);
            failedText.text = "You don't have enough money!";
        }
    }

    public void OpenConfirmPanel(string panel)
    {
        switch (diceAvailability)
        {
            case true when panel == "Dice":
                RandomBackground();
                break;
            case false when panel == "Dice":
                confirmPanel.SetActive(true);
                confirmText.text = "Are you sure you want to buy The Dice?";
                break;
            default:
            {
                if(panel == "Random")
                {
                    confirmPanel.SetActive(true);
                    confirmText.text = "Are you sure you want to buy a random color?";
                }

                break;
            }
        }

        confirmString = panel;
    }

    public void Confirm()
    {
        if (confirmString == "Dice" && GameManagerScript.playerMoney >= 1000)
        {
            RandomBackground();
            confirmPanel.SetActive(false);
        }
        else if(confirmString == "Dice" && GameManagerScript.playerMoney <= 1000)
        {
            failedPanel.SetActive(true);
            failedText.text = "You don't have enough money!";
            confirmPanel.SetActive(false);
        }
        else if(confirmString == "Random" && GameManagerScript.playerMoney >= 500)
        {
            Dice();
            confirmPanel.SetActive(false);
        }
        else
        {
            failedPanel.SetActive(true);
            failedText.text = "You don't have enough money!";
            confirmPanel.SetActive(false);
        }
    }

    public void CloseConfirmPanel()
    {
        confirmPanel.SetActive(false);
    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/BackgroundData.dat");
        BackgroundsData data = new BackgroundsData();
        ToArray();
        GameManagerScript.SaveBackground();
        GameManagerScript.Save();
        data.availability = availability;
        data.diceAvailability = diceAvailability;
        bf.Serialize(file, data);
        file.Close();

        GameManagerScript.Save();
    }

    private void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/BackgroundData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/BackgroundData.dat", FileMode.Open);
            BackgroundsData data = (BackgroundsData)bf.Deserialize(file);
            file.Close();

            availability = data.availability;
            diceAvailability = data.diceAvailability;
            count = availability.Length;
        }
        else
        {
            diceAvailability = false;
            availability[0] = true;
            for(int i = 1; i < sprites.Length; i++)
            {
                availability[i] = false;   
            }
            count = availability.Length;
        }
    }

    void BuildBGData()
    {
        for (int i = 0; i < count; i++)
        {
            backgroundsList.Add(new BgList(availability[i]));
        }

        if (count >= (sprites.Length - 1))
        {
            return;
        }
        
        for (int i = count; i < sprites.Length - 1; i++)
        {
            backgroundsList.Add(new BgList(false));
        }
    }

    private void ToArray()
    {
        for(int i = 0; i < backgroundsList.Count; i++)
        {
            availability[i] = backgroundsList[i].Availability;
        }
    }

    private void OnDisable()
    {
        GameManagerScript.Save();
        GameManagerScript.SaveBackground();
        Save();
    }
}

public class BgList
{
    public bool Availability { get; set; }

    public BgList(bool availability)
    {
        Availability = availability;
    }
}

[Serializable]
public class BackgroundsData
{
    public bool[] availability;
    public bool diceAvailability;
}
