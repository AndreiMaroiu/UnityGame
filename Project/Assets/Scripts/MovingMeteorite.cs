using UnityEngine;
using System.Collections;

public class MovingMeteorite : MonoBehaviour 
{

    public GameObject sprite;
    private GameObject player;
    private float x;
 
	void Awake ()
    {
        x = Random.Range(-2.5f, 2.5f);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        player = GameObject.FindWithTag("Player");
    }
	
	void FixedUpdate ()
    {
        if (((player.transform.position.y + 8.4) > transform.position.y) && PlayerController.enemyMeteoriteControl == true)
        {
            if (x < 0)
            {
                transform.Translate(0.01f, 0, 0);
                sprite.transform.Rotate(0, 0, 1);
            }
            if (x >= 0)
            {
                transform.Translate(-0.01f, 0, 0);
                sprite.transform.Rotate(0, 0, -1);
            }
        }  
	}
}
