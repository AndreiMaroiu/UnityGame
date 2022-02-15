using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite[] planets;
    public GameObject[] objects;
    [Range(3, 9)]
    public int planetsAmount;
    
    private readonly List<Sprite> activePlanets = new List<Sprite>();

    private void Start()
    {
        for (int i = 0; i < planets.GetLength(0); i++)
        {
            activePlanets.Add(planets[i]);
        }

        SpawnPlanets();
        RandomPosition();
    }

    private void SpawnPlanets()
    {
        for (int i = 0; i < planetsAmount; ++i)
        {
            int randomNr = Random.Range(0, activePlanets.Count - 1);

            Sprite randomPlanet = activePlanets[randomNr];

            activePlanets.RemoveAt(randomNr);

            objects[i].GetComponent<SpriteRenderer>().sprite = randomPlanet;
            objects[i].AddComponent<PolygonCollider2D>();

            objects[i].GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }

    private void RandomPosition()
    {
        for (int i = 0; i < planetsAmount; ++i)
        {
            Vector2 position = new Vector2(objects[i].transform.position.x + Random.Range(-0.5f, 0.5f), objects[i].transform.position.y + Random.Range(-0.5f, 0.5f));
            objects[i].transform.position = position;
        }
    }
}