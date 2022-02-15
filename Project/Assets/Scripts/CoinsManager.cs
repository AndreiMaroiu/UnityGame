using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CoinsManager : MonoBehaviour
{
    public static int coinsToAdd = 1;
    private static int coins = 0;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        coins = 0;
    }

    private void Update()
    {
        text.text = coins.ToString();
    }

    public static void AddPoints()
    {
        coins += coinsToAdd;
        GameManagerScript.playerMoney += coinsToAdd;
    }

    public static void Reset()
    {
        coins = 0;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("SessionMoney", coins);
    }
}
