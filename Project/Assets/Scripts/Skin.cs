using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class Skin : MonoBehaviour {

    public GameObject player;

    private List<Skins> skinsList = new List<Skins>();
    private int[] cost;
    private bool[] availability;
    private int count;

    public PlanetObject[] skins;
    public int currentSkin;
    private int randomSkin;

    public GameObject button;
    public GameObject diceCost;
    public GameObject diceFade;

    private int previewSkinNumber;
    private int playerSkinNumber;
    private bool buutonType;

    public GameObject failedPanel;
    public Text text;

    public Text skinName;
    public Text confirmText;
    public GameObject confirmPanel;
    private string confirmString;
   
    public void Preview(int changeSkinNumber)
    {
        currentSkin = changeSkinNumber;
        previewSkinNumber = currentSkin;
        if(skinsList[currentSkin].Availability == false)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
            GameManagerScript.currentSkinNumber = currentSkin;
        }
        player.GetComponent<Image>().sprite = skins[previewSkinNumber].skin;
    }

    public void SkinsManager()
    {
        if (GameManagerScript.playerMoney >= skinsList[currentSkin].Cost) 
        {
            player.GetComponent<Image>().sprite = skins[currentSkin].skin;
            GameManagerScript.playerMoney -= skinsList[currentSkin].Cost;
            skinsList[currentSkin].Cost = 0;
            skinsList[currentSkin].Availability = true;
            skins[currentSkin].cost = 0;
            skins[currentSkin].avability = true;
            GameManagerScript.currentSkinNumber = currentSkin;
            GameManagerScript.randomSkin = false;
            button.SetActive(false);

            Stats.skins += 1;

            Save();
            Stats.SaveStats();
        }
        else
        {
            failedPanel.SetActive(true);
            text.text = "You don't have enough money!";
        }
    }

    void RandomSkinNumber()
    {

        randomSkin =UnityEngine.Random.Range(1, skins.GetLength(0));

        if(randomSkin == GameManagerScript.currentSkinNumber)
        {
            randomSkin += 1;
        }
        if(randomSkin > skins.GetLength(0))
        {
            randomSkin  = skins.GetLength(0);
        }

        if (GameManagerScript.playerMoney >= skins[0].cost)
        {
            player.GetComponent<Image>().sprite = skins[randomSkin].skin;
            GameManagerScript.playerMoney -= skins[0].cost;
            GameManagerScript.currentSkinNumber = randomSkin;
            GameManagerScript.randomSkin = false;
            currentSkin = randomSkin;
            Stats.randomSkins += 1;
        }
    }

    void Dice()
    {
        if ((GameManagerScript.playerMoney >= 1000) && (!GameManagerScript.randomMeteorite))
        {
            randomSkin = UnityEngine.Random.Range(1, skins.GetLength(0));
            while (randomSkin == currentSkin)
            {
                randomSkin = UnityEngine.Random.Range(1, skins.GetLength(0));
            }
            player.GetComponent<Image>().sprite = skins[randomSkin].skin;
            GameManagerScript.playerMoney -= 1000;
            GameManagerScript.randomSkin = true;
            GameManagerScript.randomMeteorite = true;
            currentSkin = randomSkin;
            GameManagerScript.Save();
            GameManagerScript.SaveBackground();
            diceCost.SetActive(false);
            diceFade.SetActive(false);
        }
        else if (GameManagerScript.randomMeteorite)
        {
            randomSkin = UnityEngine.Random.Range(1, skins.GetLength(0));
            while (randomSkin == currentSkin)
            {
                randomSkin = UnityEngine.Random.Range(1, skins.GetLength(0));
            }
            player.GetComponent<Image>().sprite = skins[randomSkin].skin;
            GameManagerScript.randomSkin = true;
            GameManagerScript.randomMeteorite = true;
            currentSkin = randomSkin;
            GameManagerScript.Save();
            GameManagerScript.SaveBackground();
        }
        
        
    }

    public void ConfirmPanel(string panel)
    {

        if (panel == "Dice" && GameManagerScript.randomMeteorite == false)
        {
            confirmText.text = "Are you sure you want to buy The Dice?";
            confirmPanel.SetActive(true);
        }
        else if(panel == "Dice" && GameManagerScript.randomMeteorite)
        {
            Dice();
        }
        else if (panel == "Random")
        {
            confirmText.text = "Are you sure you want to buy a random meterite?";
            confirmPanel.SetActive(true);
        }
        confirmString = panel;
    }

    public void ConfirmPurchase()
    {
        if (confirmString == "Dice" && GameManagerScript.playerMoney >= 1000)
        {
            Dice();
            confirmPanel.SetActive(false);
        }
        else if (confirmString == "Dice" && GameManagerScript.playerMoney <= 1000)
        {
            failedPanel.SetActive(true);
            text.text = "You don't have enough money!";
            confirmPanel.SetActive(false);
        }
        else if (confirmString == "Random" && GameManagerScript.playerMoney >= 250)
        {
            RandomSkinNumber();
            confirmPanel.SetActive(false);
        }
        else
        {
            failedPanel.SetActive(true);
            text.text = "You don't have enough money!";
            confirmPanel.SetActive(false);
        }
        
    }

    public void CloseConfirmPanel()
    {
        confirmPanel.SetActive(false);
    }

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        playerSkinNumber = GameManagerScript.currentSkinNumber;
        currentSkin = playerSkinNumber;
        player.GetComponent<Image>().sprite = skins[playerSkinNumber].skin;
        button.SetActive(false);
        skinName.text = skins[currentSkin].skinName;
        cost = new int[skins.Length];
        availability = new bool[skins.Length];
        Load();
        BuildSkinsData();
        Debug.Log(GameManagerScript.currentSkinNumber);
        diceCost.SetActive(!GameManagerScript.randomMeteorite);
        confirmPanel.SetActive(false);
        diceFade.SetActive(!GameManagerScript.randomColor);
    }

    void Update()
    {
        if (skins[currentSkin].rotating == true)
        {
            player.transform.Rotate(0, 0, 0.3f);
        }
        if (skins[currentSkin].rotating == false)
        {
            player.transform.rotation = new Quaternion();
        }
        skinName.text = skins[currentSkin].skinName;
    }

    void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/Skins.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Skins.dat", FileMode.Open);
            PlayerData playerdata = (PlayerData)bf.Deserialize(file);
            file.Close();

            cost = playerdata.cost;
            availability = playerdata.availability;
            count = playerdata.cost.Length;
        }
        else
        {
            for(int i = 0; i < skins.Length; i++)
            {
                cost[i] = skins[i].cost;
                availability[i] = skins[i].avability;
            }
            count = cost.Length;
        }
    }

    void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Skins.dat");
        PlayerData playerData = new PlayerData();
        ToArray();
        playerData.cost = cost;
        playerData.availability = availability;

        bf.Serialize(file, playerData);
        file.Close();
    }

    void ToArray()
    {
        for(int i = 0;i < skinsList.Count; i++)
        {
            cost[i] = skinsList[i].Cost;
            availability[i] = skinsList[i].Availability;
        }
    }

    void BuildSkinsData()
    {
        for (int i = 0; i < count; i++)
        {
            skinsList.Add(new Skins(cost[i], availability[i]));
        }

        if (count < skins.Length)
        {
            for (int i = count; i < skins.Length; i++)
            {
                skinsList.Add(new Skins(skins[i].cost, skins[i].avability));
            }
        }
        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].avability = skinsList[i].Availability;
        }
    }

    void OnDisable()
    {
        GameManagerScript.Save();
        GameManagerScript.SaveBackground();
        Save();
        Stats.SaveStats();
    }
}

public class Skins
{
    public int Cost { get; set; }
    public bool Availability { get; set; }

    public Skins ( int cost , bool availability)
    {
        Cost = cost;
        Availability = availability;
    }
}

[Serializable]
class PlayerData
{
    public int[] cost;
    public bool[] availability;
}