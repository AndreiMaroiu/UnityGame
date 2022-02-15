using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class GameManagerScript : MonoBehaviour 
{
    public static int playerMoney;
    public static int currentSkinNumber;
    public static int highScore;
    public static int bgColor;
    public static bool randomColor;
    public static bool randomSkin;
    public static bool randomMeteorite;

    public static float masterVol;
    public static float musicVol;
    public static float sfxVol;

    [SerializeField] private AudioMixer mixer;

    private void Awake()
    {
        Load();
        LoadBackground();

        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("SoundToggle"))
        {
            PlayerPrefs.SetInt("SoundToggle", 1);
        }
        
        AudioListener.pause = (PlayerPrefs.GetInt("SoundToggle") != 1);
        
    }

    private void Start()
    {
        masterVol = PlayerPrefs.GetFloat("MasterVol");
        musicVol = PlayerPrefs.GetFloat("MusicVol");
        sfxVol = PlayerPrefs.GetFloat("SfxVol");
        
        mixer.SetFloat("MasterVol", masterVol);
        mixer.SetFloat("musicVol", musicVol);
        mixer.SetFloat("sfxVol", sfxVol);
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create($"{Application.persistentDataPath}/PlayerProgress.dat");
        
        PlayerProgress data = new PlayerProgress
        {
            currentSkin = currentSkinNumber, 
            money = playerMoney, 
            score = highScore
        };
        
        bf.Serialize(file, data);
        file.Close();
        SaveBackground();
    }

    public static void SaveBackground()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create($"{Application.persistentDataPath}/Background.dat");
        
        BackgroundStats data = new BackgroundStats()
        {
            backgroundColor = bgColor,
            randomColor = randomColor,
            randomSkins = randomSkin,
            randomMeteorite = randomMeteorite
        };
        
        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadBackground()
    {
        if (File.Exists($"{Application.persistentDataPath}/Background.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open($"{Application.persistentDataPath}/Background.dat", FileMode.Open);
            BackgroundStats data = bf.Deserialize(file) as BackgroundStats;
            file.Close();

            if (data != null)
            {
                bgColor = data.backgroundColor;
                randomColor = data.randomColor;
                randomSkin = data.randomSkins;
                randomMeteorite = data.randomMeteorite;
            }
        }
        else
        {
            bgColor = 0;
            randomColor = false;
            randomSkin = false;
            randomMeteorite = false;
        }
        
        Debug.Log(randomColor.ToString());
    }

    public static void Load()
    {
        if(File.Exists($"{Application.persistentDataPath}/PlayerProgress.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open($"{Application.persistentDataPath}/PlayerProgress.dat", FileMode.Open);
            PlayerProgress data = bf.Deserialize(file) as PlayerProgress;
            file.Close();

            if (data != null)
            {
                currentSkinNumber = data.currentSkin;
                playerMoney = data.money;
                highScore = data.score;
            }
        }
        else
        {
            currentSkinNumber = 0;
            playerMoney = 0;
        }
    }

    private void OnDisable()
    {
        Save();
        SaveBackground();
    }
}

[Serializable]
public class PlayerProgress
{
    public int money;
    public int currentSkin;
    public int score;
}

[Serializable]
public class BackgroundStats
{
    public int backgroundColor;
    public bool randomColor;
    public bool randomSkins;
    public bool randomMeteorite;
}
