
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����Ÿ�Կ� ���ذ� ������ȵȴ�.
/// ���� ���� �ε����� 0��Ұ��� �����÷� �����̴�.
/// </summary>
public enum eWeaponType
{
    None = 0, 
    Rifl = 1,
    Bazooka = 2,
    Handgun = 3,
}

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [SerializeField]
    public List<AnimatorClipInfo> list = new List<AnimatorClipInfo>();  

    public List<RuntimeAnimatorController> list2 = new List<RuntimeAnimatorController>();

    public Dictionary<eWeaponType, RuntimeAnimatorController> dic = new Dictionary<eWeaponType, RuntimeAnimatorController>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        
        for(int i = 0; i < list2.Count; i++)
        {
            dic.Add((eWeaponType)i, list2[i]);
        }

        

        

    }



}
