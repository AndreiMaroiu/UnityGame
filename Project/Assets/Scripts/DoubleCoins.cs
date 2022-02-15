using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoins : MonoBehaviour 
{
    public int currentPowerUp;
    private PowerUpsHelp helper;

    private void Start()
    {
        helper = GameObject.FindWithTag("Helper").GetComponent<PowerUpsHelp>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            Debug.Log("double coins");
            CoinsManager.coinsToAdd = 2;
            helper.SetPowerUp(currentPowerUp, gameObject);
            helper.Activate();
        }
    }

    // private IEnumerator Cooldown()
    // {
    //     yield return new WaitForSeconds(SavePowerUps.time[currentPowerUp]);
    //     CoinsManager.coinsToAdd = 1;
    // }
}
