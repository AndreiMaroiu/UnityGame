using System;
using UnityEngine;

public sealed class DoubleCoinsPowerUp : MonoBehaviour
{
    [SerializeField]
    private float powerUpTime;

    private PowerUp powerUp;

    private void Start()
    {
        powerUp = new PowerUp(OnStartAction, OnEndAction, powerUpTime);
    }

    private void OnStartAction()
    {
        CoinsManager.coinsToAdd *= 2;
    }

    private void OnEndAction()
    {
        CoinsManager.coinsToAdd /= 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(powerUp.StartPowerUp());
        }
    }
}
