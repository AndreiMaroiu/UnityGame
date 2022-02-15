using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastTraver : MonoBehaviour 
{
    public int powerUpNumber;
    private GameObject player;
    private PowerUpsHelp helper;

    private void Start()
    {
        //player = GameObject.FindWithTag("Player");
        //helper = GameObject.FindWithTag("Helper").GetComponent<PowerUpsHelp>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player.GetComponent<CircleCollider2D>().enabled = !player.GetComponent<CircleCollider2D>().enabled;
            PlayerController.moveV = 0;
            helper.SetPowerUp(0, gameObject);
            helper.Activate();
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(SavePowerUps.time[0]);
        player.GetComponent<CircleCollider2D>().enabled = !player.GetComponent<CircleCollider2D>().enabled;
    }

}
