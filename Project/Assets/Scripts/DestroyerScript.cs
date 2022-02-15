using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour
{

    public float[] coinsCoordonates;
    public GameObject coinPrefab;

    public GameObject[] prefabs;
    public float[] coordonates;
    private List<GameObject> planets;
    public Transform point;
    public Transform generatePoint;

    public Camera mainCamera;
    public GameObject[] destroyers;

    private void Start()
    {
        planets = new List<GameObject>();
        Vector3 planetTransform;

        for (int i = 0; i < 5; i++)
        {
            Instantiate(coinPrefab, new Vector3(coinsCoordonates[Random.Range(0, coinsCoordonates.Length)], (i + 1) * 5, 10f), transform.rotation);
        }

        for (int p = 0; p < 10; p++)
        {
            GameObject obj = Instantiate(prefabs[p]);
            obj.SetActive(false);
            planets.Add(obj);
        }

        for (int i = 0; i < 3; i++)
        {
            int randomPlanet = Random.Range(0, prefabs.Length);

            while (planets[randomPlanet].activeInHierarchy)
            {
                randomPlanet = Random.Range(0, prefabs.Length);
            }

            planetTransform = new Vector3(coordonates[Random.Range(0, coordonates.Length)], (i + 1) * 5, -5f);
            planets[randomPlanet].transform.position = planetTransform;
            planets[randomPlanet].SetActive(true);
        }

        if (mainCamera.aspect == 0.5625)
        {
            for (int i = 0; i < 3; i++)
            {
                destroyers[i].GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void Update()
    {
        if (generatePoint.position.y <= point.position.y)
        {
            generatePoint.transform.position = new Vector3(0f, generatePoint.transform.position.y + 5, 0f);
            GetPooledObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyPlanet") || other.CompareTag("Meteorite"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Coin"))
        {
            other.transform.position = new Vector3(coinsCoordonates[Random.Range(0, coinsCoordonates.Length)], other.transform.position.y + 25, 10f);
        }
    }

    public void GetPooledObject()
    {
        int randomPlanet = Random.Range(0, prefabs.GetLength(0));

        while (planets[randomPlanet].activeInHierarchy)
        {
            randomPlanet = Random.Range(0, prefabs.GetLength(0));
        }

        planets[randomPlanet].transform.position = new Vector3(coordonates[Random.Range(0, coordonates.Length)], generatePoint.transform.position.y, -5f);
        planets[randomPlanet].SetActive(true);
    }
}