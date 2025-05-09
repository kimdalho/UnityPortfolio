using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private readonly Queue<GameObject> _pool = new Queue<GameObject>();
    private readonly GameObject _prefab;
    private readonly Transform _parent;

    public ObjectPool(GameObject prefab, int initialCount = 10, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < initialCount; i++)
        {
            var obj = GameObject.Instantiate(_prefab, _parent);                       

            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        GameObject obj = _pool.Count > 0 ? _pool.Dequeue() : GameObject.Instantiate(_prefab, _parent);

        obj.SetActive(true);

        // IPoolable이 있다면 호출
        foreach (var p in obj.GetComponents<IPoolable>())
            p.OnSpawn();

        return obj;
    }

    public void Return(GameObject obj)
    {
        foreach (var p in obj.GetComponents<IPoolable>())
            p.OnDespawn();

        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}