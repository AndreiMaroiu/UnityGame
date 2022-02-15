using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Power Up", menuName = "Scriptables/PowerUp Object")]
public class PowerUpObject : ScriptableObject 
{
    public Sprite sprite;
    public string powerUpName;
    public int lvl;
    public int price;
    public int time;
}
