
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ���� ����Ÿ�Կ� ���ذ� ������ȵȴ�.
/// ���� ���� �ε����� 0��Ұ��� �����÷� �����̴�.
/// </summary>
public enum eWeaponType
{
    Punch = 0, 
    Rifl = 1,
    Bazooka = 2,
    Handgun = 3,
}

public class ResourceManager : MonoBehaviour
{
    //public static ResourceManager Instance;
    private static ResourceManager _instance;

    [SerializeField]
    public List<AnimatorClipInfo> list = new List<AnimatorClipInfo>();  

    public List<RuntimeAnimatorController> list2 = new List<RuntimeAnimatorController>();

    //0����5������ �Ӹ� ������
    //6���� ������ �ٵ������
    
    public List<PickupItemData> pickupItemDatas = new List<PickupItemData>();
    public List<PickupWeaponItemData> weaponPickupItemDatas = new List<PickupWeaponItemData>();
    [Header("�� ������Ʈ")]
    public GameObject HeadItemPrefab;
    public GameObject BodyItemPrefab;
    public List<GameObject> WeaponItemPrefab;

    public List<GameObject> monsters;
    public GameObject FlyPrefab;
    
    private Dictionary<eEuipmentType, boxInfo> itemInfos;

    public Dictionary<eWeaponType, RuntimeAnimatorController> dic = new Dictionary<eWeaponType, RuntimeAnimatorController>();


    private class boxInfo
    {
       public GameObject prefab;
       public Type compo;

        public boxInfo(GameObject obj,Type type)
        {
            this.prefab = obj;
            compo = type;   
        }
    }



    public static ResourceManager Instance
    {
        get
        {
#if UNITY_EDITOR
            // �÷��� ������ �����ϰ� ���� (Editor������)
            if (!Application.isPlaying)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ResourceManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("[ResourceManager Editor Instance]");
                        _instance = obj.AddComponent<ResourceManager>();
                        obj.hideFlags = HideFlags.HideAndDontSave; // �� ���� X
                    }
                }
                return _instance;
            }
#endif
            // �Ϲ� ��Ÿ�ӿ�
            if (_instance == null)
            {
                GameObject obj = new GameObject("[ResourceManager]");
                _instance = obj.AddComponent<ResourceManager>();
                
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    private void ItemPrefabSetup()
    {
        itemInfos = new Dictionary<eEuipmentType, boxInfo>();
        itemInfos.Add(eEuipmentType.Head,new boxInfo(HeadItemPrefab, typeof(HeadItem)));
        itemInfos.Add(eEuipmentType.Body,new boxInfo(BodyItemPrefab, typeof(BodyItem)));
    }
    

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        _instance = this;

        for (int i = 0; i < list2.Count; i++)
        {
            dic.Add((eWeaponType)i, list2[i]);
        }
        ItemPrefabSetup();
    }

    public GameObject CreateItemToTier(int tier, Transform parent)
    {
        if(itemInfos == null)
        {
            ItemPrefabSetup();
        }

        System.Random rand = new System.Random();
        var result = pickupItemDatas.Where(_ => _.Rank == tier)
        .OrderBy(x => rand.Next())
        .Take(1)
        .ToList();

        var data = itemInfos[result[0].eEquipmentType];
       
        GameObject item = Instantiate(data.prefab);
        item.transform.SetParent(parent);               
        item.transform.localScale = Vector3.one;
        // compo�� type ������
        var itemCompo = item.GetComponent(data.compo) as EquipmentItem;
        itemCompo.Init(result[0]);
        return item;
    }


    public GameObject CreateItemToIndex(int index, Transform parent)
    {
        if (index > pickupItemDatas.Count - 1)
            return null;    

        var result = pickupItemDatas[index];

        var data = itemInfos[result.eEquipmentType];

        GameObject item = Instantiate(data.prefab);
        item.transform.SetParent(parent);
        item.transform.localScale = Vector3.one;
        // compo�� type ������
        var itemCompo = item.GetComponent(data.compo) as EquipmentItem;
        itemCompo.Init(result);
        return item;
    }



    public GameObject CreateweaponItemToIndex(int index, Transform parent)
    {
        if (index > weaponPickupItemDatas.Count-1)
            return null;

        var result = weaponPickupItemDatas[index];
        var reuslt1 = WeaponItemPrefab[index];
        GameObject NewWeapon = Instantiate(reuslt1);

        NewWeapon.transform.SetParent(parent);
        NewWeapon.transform.position = parent.GetComponent<GridNode>().GetItemPos();

        var itemCompo = NewWeapon.GetComponent<EquipmentItem>();
        itemCompo.Init(result);
        return NewWeapon;
    }


    public GameObject CreateMonster(Transform parent)
    {       
        System.Random rnd = new System.Random();

        var targetMonster = monsters[rnd.Next(monsters.Count)];
        GameObject model = Instantiate(targetMonster);
        return model;
    }

    public Fly CreateFly()
    {
        GameObject Fly = Instantiate(FlyPrefab);
        return Fly.GetComponent<Fly>();
    }

}
