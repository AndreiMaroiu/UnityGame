using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SavePowerUps : MonoBehaviour {

    public PowerUpObject[] powerUps;

    private List<PowerUps> list = new List<PowerUps>();
    public static int[] lvl;
    public static int[] price;
    public static int[] time;

    private int count;

	void Start ()
    {
        lvl = new int[powerUps.Length];
        price = new int[powerUps.Length];
        time = new int[powerUps.Length];
        Debug.Log(powerUps.Length);
        Load();
        BuildData();
    }

    void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PowerUps.dat");
        Data data = new Data();
        ToArray();
        data.lvl = lvl;
        data.price = price;
        data.time = time;

        bf.Serialize(file, data);
        file.Close();
    }

    void Load()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + "/PowerUps.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/PowerUps.dat", FileMode.Open);
                Data data = (Data)bf.Deserialize(file);
                file.Close();

                lvl = data.lvl;
                price = data.price;
                time = data.time;
                count = data.price.GetLength(0);
            }
            else
            {
                for (int i = 0; i < powerUps.Length; i++)
                {
                    lvl[i] = powerUps[i].lvl;
                    price[i] = powerUps[i].price;
                    time[i] = powerUps[i].time;
                }
                count = powerUps.Length;
            }
        }
        catch { }
    }

    void ToArray()
    {
        for(int i = 0; i < list.Count; i++)
        {
            lvl[i] = list[i].Lvl;
            price[i] = list[i].Price;
            time[i] = list[i].Time;
        }
    }

    void BuildData()
    {
        for (int i = 0; i < count; i++)
        {
            list.Add(new PowerUps(lvl[i], price[i], time[i]));
        }
        if(count < powerUps.Length)
        {
            for (int i = count; i < powerUps.Length; i++)
            {
                list.Add(new PowerUps(powerUps[i].lvl, powerUps[i].price, powerUps[i].time));
            }
        }
    }


    private void OnDisable()
    {
        Save();
    }

}


class PowerUps
{
    public int Lvl { get; set; }
    public int Price { get; set; }
    public int Time { get; set; }

    public PowerUps(int lvl , int price, int time)
    {
        Lvl = lvl;
        Price = price;
        Time = time;
    }
}

[System.Serializable]
class Data
{
    public int[] lvl;
    public int[] price;
    public int[] time;
}