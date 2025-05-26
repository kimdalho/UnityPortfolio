using System.Collections.Generic;
using UnityEngine;

public class RuntimeResourceManager : MonoBehaviour, IResourceProvider
{
    //C# 9.0부터 제공
    private Dictionary<string, GameObject> _prefabs = new();
    public void Register(string key, GameObject prefab)
    {
        _prefabs[key] = prefab;
    }

    public GameObject GetPrefab(string key)
    {
        return _prefabs.TryGetValue(key, out var prefab) ? prefab : null;
    }
}