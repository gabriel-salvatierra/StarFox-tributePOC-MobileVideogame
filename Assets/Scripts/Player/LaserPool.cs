using System.Collections.Generic;
using UnityEngine;

public static class LaserPool
{
    private static Dictionary<LaserProjectile, Queue<LaserProjectile>> _pools
        = new Dictionary<LaserProjectile, Queue<LaserProjectile>>();

    public static LaserProjectile Get(LaserProjectile prefab)
    {
        if (!_pools.ContainsKey(prefab))
            _pools[prefab] = new Queue<LaserProjectile>();

        if (_pools[prefab].Count > 0)
            return _pools[prefab].Dequeue();

        return GameObject.Instantiate(prefab);
    }

    public static void Return(LaserProjectile laser)
    {
        laser.gameObject.SetActive(false);
        _pools[laser] ??= new Queue<LaserProjectile>();
        _pools[laser].Enqueue(laser);
    }
}
