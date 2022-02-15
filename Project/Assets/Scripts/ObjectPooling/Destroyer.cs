using System.Collections.Generic;
using UnityEngine;

public sealed class Destroyer : MonoBehaviour
{
    [SerializeField]
    private bool canDestroy = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canDestroy)
        {
            return;
        }

        switch(other.tag)
        {
            case "Coin":
                Pools.CoinPool.Return(other.gameObject);
                break;
            case "Meteorite":
            case "EnemyPlanet":
                Pools.PlanetPool.Return(other.gameObject);
                break;
            case "PickUp":
                Pools.PowerUpsPool.Return(other.gameObject);
                break;
        }
    }
}
