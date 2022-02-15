using System;
using UnityEngine;

public sealed class FastTravelPowerUp : MonoBehaviour
{
    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    private float powerUpTime;

    private PlayerController meteorite;
    private PowerUp powerUp;


    private void Start()
    {
        powerUp = new PowerUp(OnStartAction, OnEndAction, powerUpTime);
        meteorite = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
    }

    private void OnStartAction()
    {
        meteorite?.MultiplySpeed(speedMultiplier);
    }

    private void OnEndAction()
    {
        meteorite?.MultiplySpeed(-speedMultiplier);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(powerUp.StartPowerUp());
        }
    }
}
