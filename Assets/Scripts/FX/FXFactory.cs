using System.Collections.Generic;
using UnityEngine;

public class FXFactory : MonoBehaviour
{
    #region Singleton
    public static FXFactory Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
        fxPrefabs.Add("Attack", prefabs[0]);
        fxPrefabs.Add("FanShapeFire", prefabs[1]);
        fxPrefabs.Add("Explosion", prefabs[2]);
        fxPrefabs.Add("FlameThrower", prefabs[3]);
        fxPrefabs.Add("Shot", prefabs[4]);
    }
    #endregion

    [SerializeField] private GameObject[] prefabs;

    private Dictionary<string, GameObject> fxPrefabs = new Dictionary<string, GameObject>();

    private GameObject GetPrefab(string abilityTag)
    {
        if (!fxPrefabs.TryGetValue(abilityTag, out var _prefab))
        {
            Debug.LogError($"{abilityTag} is not Exist in Keys");
        }

        return _prefab;
    }

    public GameObject GetFX(string abilityTag, Transform parent, bool isWorldPos = false)
    {
        return Instantiate(GetPrefab(abilityTag), parent, isWorldPos);
    }

    public GameObject GetFX(string abilityTag, Vector3 pos, Quaternion rot)
    {
        return Instantiate(GetPrefab(abilityTag), pos, rot);
    }

    public GameObject GetFX(eTagType abilityTag, Transform parent, bool isWorldPos = false)
    {
        return Instantiate(GetPrefab(abilityTag.ToString()), parent, isWorldPos);
    }

    public GameObject GetFX(eTagType abilityTag, Vector3 pos, Quaternion rot)
    {
        return Instantiate(GetPrefab(abilityTag.ToString()), pos, rot);
    }
}
