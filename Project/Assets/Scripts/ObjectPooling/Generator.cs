using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Spawner Fields")]
    [SerializeField]
    [Range(1, 5)]
    private float distance = 2.0f;

    [SerializeField]
    [Range(2, 10)]
    private float spawnDistance = 5.0f;

    [SerializeField]
    private Transform spawnAux;

    [SerializeField]
    private Transform spawnPoint;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private GameObject[] planets;

    [SerializeField]
    private GameObject[] powerUps;

    [Space]
    [SerializeField]
    private bool canSpawn = true;

    private float speed = 5.0f;

    private IObjectPool coinPool;
    private IObjectPool planetPool;
    private IObjectPool powerUpPool;
    private PositionsList list;

    private void Start()
    {
        coinPool = new SimplePool(15, coin);
        Pools.CoinPool = coinPool;

        planetPool = new RandomPool(planets);
        Pools.PlanetPool = planetPool;

        powerUpPool = new RandomPool(powerUps, 3);
        Pools.PowerUpsPool = powerUpPool;
    }

    private Vector3 GetPosition(int offset, bool randomizedPosition = false)
    {
        float distance = this.distance;
        if (randomizedPosition)
        {
            distance += Random.Range(-0.5f, 0.5f);
        }

        return spawnPoint.position + Vector3.right * distance * offset;
    }

    private void SpawnObject(IObjectPool pool, bool randomizedPosition = false)
    {
        if (list.HasNext())
        {
            int next = list.TakeRandom();
            pool.Get(GetPosition(next, randomizedPosition));
        }
    }

    private void Spawn()
    {
        if (canSpawn)
        {
            PositionsHelper.GeneratorPositions();
            list = PositionsHelper.List;

            SpawnObject(planetPool);
            SpawnObject(coinPool, randomizedPosition: true);
            SpawnObject(powerUpPool);
        }
    }

    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if (spawnAux.position.y >= spawnPoint.position.y)
        {
            Spawn();
            spawnPoint.position += Vector3.up * spawnDistance;
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnAux != null)
        {
            const string iconName = "generatorIcon.png";

            Vector3 middle = spawnAux.position;
            Vector3 left = middle + Vector3.left * distance;
            Vector3 right = middle + Vector3.right * distance;

            Gizmos.DrawIcon(left, iconName);
            Gizmos.DrawIcon(middle, iconName);
            Gizmos.DrawIcon(right, iconName);
        }

        if (spawnPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawnPoint.position, 0.5f);
            Gizmos.DrawSphere(spawnPoint.position + Vector3.up * spawnDistance, 0.5f);
            Gizmos.color = Color.white;
        }
    }

    public void Return(GameObject gameObject)
    {
        coinPool.Return(gameObject);
    }
}
