using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Transform spawnAux;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private GameObject[] objects;

    [SerializeField]
    private float spawnDistance = 5;

    [SerializeField]
    private bool canSpawn = true;

    private IObjectPool pool;

    private void Start()
    {
        pool = new RandomPool(objects);
    }

    private void Update()
    {
        transform.position += Vector3.up * 5 * Time.deltaTime;

        if (spawnAux.position.y >= spawnPoint.position.y)
        {
            spawnPoint.position += Vector3.up * spawnDistance;

            if (canSpawn)
            {
                pool.Get(spawnAux.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(spawnPoint.position, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnAux.position, 1);
    }

    public void Return(GameObject gameObject)
    {
        pool.Return(gameObject);
    }
}
