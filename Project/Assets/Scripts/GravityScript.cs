using UnityEngine;
using System.Collections;


public class GravityScript : MonoBehaviour
{
    public float gravitationalForce;
    private Vector3 directionOfBirdFromPlanet;
    
    void Start()
    {
        directionOfBirdFromPlanet = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerController.reviveCooldownFroScore == false)
        {
            if (other.tag == "Player")
            {
                directionOfBirdFromPlanet = (transform.position - other.transform.position).normalized;
                other.GetComponent<Rigidbody2D>().AddRelativeForce(directionOfBirdFromPlanet * gravitationalForce);
                other.GetComponent<Rigidbody2D>().IsAwake();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(0, 0, 0));
            other.GetComponent<Rigidbody2D>().Sleep();
        }
    }
}
