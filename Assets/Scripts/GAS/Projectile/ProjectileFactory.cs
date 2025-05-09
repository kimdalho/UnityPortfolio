using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Bullet,
    NinjaStars,
    BazookaMissile,
    ALL // ��� ����ü �� �������� ���� �� ���
}

public class ProjectileFactory : MonoBehaviour , IPoolable
{
    #region Singleton
    public static ProjectileFactory Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("Projectile Type ������� ����")]
    [SerializeField] private List<Projectile> projectilePrefabs;

    private int GetIndex(ProjectileType type)
    {
        var _index = (int)type;

        if (_index < 0 || _index >= projectilePrefabs.Count)
        {
#if UNITY_EDITOR
            Debug.LogError("Out of Range");
#endif
            _index = 0;
        }

        return _index;
    }

    public Projectile GetProjectile(ProjectileType type, Transform parent, bool isWorldPos = false)
    {
        return Instantiate(projectilePrefabs[GetIndex(type)], parent, isWorldPos);
    }

    public Projectile GetProjectile(ProjectileType type, Vector3 pos, Quaternion rot)
    {
        return Instantiate(projectilePrefabs[GetIndex(type)], pos, rot);
    }

    public void OnSpawn()
    {
        throw new System.NotImplementedException();
    }

    public void OnDespawn()
    {
        throw new System.NotImplementedException();
    }
}
