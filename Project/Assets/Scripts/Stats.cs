using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Stats : MonoBehaviour 
{
    public static int money;
    public static float averageMoney;
    public static int games;
    public static int skins;
    public static int randomSkins;
    public static int backgrounds;
    public static int randomBackgrounds;
    public static int highscore;
    public static float averageScore;
    public static int powerups;

    void Awake ()
    {
        LoadStats();
	}

	public static void SaveStats ()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Stats.dat");
        PlayerStats data = new PlayerStats();

        data.money = money;
        data.averageMoney = averageMoney;
        data.games = games;
        data.skins = skins;
        data.randomSkins = randomSkins;
        data.backgrounds = backgrounds;
        data.randomBackgrounds = randomBackgrounds;
        data.highscore = highscore;
        data.averageScore = averageScore;
        data.powerups = powerups;

        bf.Serialize(file, data);
        file.Close();
	}

    public static void LoadStats()
    {
        if(File.Exists(Application.persistentDataPath + "/Stats.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Stats.dat", FileMode.Open);
            PlayerStats data = (PlayerStats)bf.Deserialize(file);
            file.Close();

            money = data.money;
            averageMoney = data.averageMoney;
            games = data.games;
            skins = data.skins;
            randomSkins = data.randomSkins;
            backgrounds = data.backgrounds;
            randomBackgrounds = data.randomBackgrounds;
            highscore = data.highscore;
            averageScore = data.averageScore;
            powerups = data.powerups;
        }
        else
        {
            money = 0;
            games = 0;
            skins = 1;
            randomSkins = 0;
            backgrounds = 1;
            randomBackgrounds = 0;
            highscore = 0;
            averageScore = 0;
            powerups = 1;
        }
    }

    void OnDisable()
    {
        SaveStats();
    }
}

[Serializable]
class PlayerStats
{
    public int money;
    public float averageMoney;
    public int games;
    public int skins;
    public int randomSkins;
    public int backgrounds;
    public int randomBackgrounds;
    public int highscore;
    public float averageScore;
    public int powerups;
}
