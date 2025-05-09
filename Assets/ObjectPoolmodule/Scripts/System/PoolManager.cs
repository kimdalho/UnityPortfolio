using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 오브젝트 풀링을 서비스하는 싱글톤 클래스
/// </summary>
public class PoolManager : MonoBehaviour
{
    private class Pool
    {
        private readonly Queue<GameObject> _objects = new();
        private readonly GameObject _prefab;
        private readonly Transform _parent;

        public Pool(GameObject prefab, int preloadCount = 10)
        {
            _prefab = prefab;
            _parent = new GameObject($"{prefab.name}_Pool").transform;

            for (int i = 0; i < preloadCount; i++)
            {
                var obj = GameObject.Instantiate(_prefab, _parent);
                obj.SetActive(false);
                _objects.Enqueue(obj);
            }
        }

        public GameObject Get()
        {
            var obj = _objects.Count > 0 ? _objects.Dequeue() : GameObject.Instantiate(_prefab);
            obj.transform.SetParent(null);
            obj.SetActive(true);
            return obj;
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(_parent);
            _objects.Enqueue(obj);
        }
    }

    private readonly Dictionary<string, Pool> _pools = new();

    public void CreatePool(string key, GameObject prefab, int preloadCount = 10)
    {
        if (!_pools.ContainsKey(key))
            _pools[key] = new Pool(prefab, preloadCount);
    }

    public GameObject Get(string key)
    {
        if (_pools.TryGetValue(key, out var pool))
            return pool.Get();

        Debug.LogError($"Pool not found for key: {key}");
        return null;
    }

    public void Return(string key, GameObject obj)
    {
        if (_pools.TryGetValue(key, out var pool))
            pool.Return(obj);
        else
            Destroy(obj); // fallback
    }
}