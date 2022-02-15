using UnityEngine;

public class CoinsPickUp : MonoBehaviour
{
    [SerializeField] 
    private int pointsToAddToScore;
    [SerializeField] 
    private float[] coordonates;

    private void OnTriggerEnter2D (Collider2D other)
    { 
        if (other.CompareTag("Player"))
        {
            CoinsManager.AddPoints();
            transform.position = new Vector3(coordonates[Random.Range(0 , coordonates.Length)], transform.position.y + 25, 10f);
            ScoreManager.AddPoints(pointsToAddToScore);
        }
    }
}