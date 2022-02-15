using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Pools
{
    private static IObjectPool coinPool = null;
    private static IObjectPool planetPool = null;
    private static IObjectPool powerUpsPool = null;

    public static IObjectPool CoinPool
    {
        get => coinPool;

        set
        {
            if (value != null)
            {
                coinPool = value;
            }
        }
    }

    public static IObjectPool PlanetPool
    {
        get => planetPool;

        set
        {
            if (value != null)
            {
                planetPool = value;
            }
        }
    }

    public static IObjectPool PowerUpsPool
    {
        get => powerUpsPool;

        set
        {
            if (value != null)
            {
                powerUpsPool = value;
            }
        }
    }
}
