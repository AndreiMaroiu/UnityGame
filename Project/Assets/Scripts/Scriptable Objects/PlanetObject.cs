using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Planet", menuName = "Scriptables/Planet Object")]
public class PlanetObject : ScriptableObject
{
    public Sprite skin;
    public int cost;
    public bool avability;
    public bool rotating;
    public string skinName;
}