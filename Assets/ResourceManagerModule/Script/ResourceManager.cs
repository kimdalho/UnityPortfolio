using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 무기 열거타입에 오해가 있으면안된다.
/// 모델의 웨폰 인덱스는 0요소값에 라이플로 시작이다.
/// </summary>


public class ResourceManager : MonoBehaviour
{    
    private static ResourceManager _instance;    
    /// <summary>
    /// 공급자 분기 타입에 따라 서비스가 달라진다.
    /// </summary>
    private static IResourceProvider _provider;

    [SerializeField]
    public List<AnimatorClipInfo> list = new List<AnimatorClipInfo>();  

    public List<RuntimeAnimatorController> list2 = new List<RuntimeAnimatorController>();

    //0에서5까지는 머리 아이템
    //6부터 끝까지 바디아이템
    
    public List<PickupItemData> pickupItemDatas = new List<PickupItemData>();
    public List<PickupWeaponItemData> weaponPickupItemDatas = new List<PickupWeaponItemData>();
    [Header("모델 오브젝트")]
    public GameObject HeadItemPrefab;
    public GameObject BodyItemPrefab;
    public List<GameObject> WeaponItemPrefab;
    public MonsterFactory monsterFactory;
    public RoomFactory roomFactory;

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
            // 플레이 전에도 동작하게 만듦 (Editor에서만)
            if (!Application.isPlaying)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ResourceManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("[ResourceManager Editor Instance]");
                        _instance = obj.AddComponent<ResourceManager>();
                        obj.hideFlags = HideFlags.HideAndDontSave; // 씬 저장 X
                    }
                }
                return _instance;
            }
#endif
            // 일반 런타임용
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
        // compo는 type 변수임
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
        // compo는 type 변수임
        var itemCompo = item.GetComponent(data.compo) as EquipmentItem;
        itemCompo.Init(result);
        return item;
    }

    public GameObject CreateWeaponItemToIndex(int index, Transform parent)
    {
        if (index > weaponPickupItemDatas.Count-1)
            return null;

        var result = weaponPickupItemDatas[index];
        var reuslt1 = WeaponItemPrefab[index];
        GameObject NewWeapon = Instantiate(reuslt1);

        NewWeapon.transform.SetParent(parent);
        NewWeapon.transform.position = parent.GetComponent<GridNode>().GetItemPos();

        var itemCompo = NewWeapon.GetComponent<WeaponItem>();
        itemCompo.Init(result);
        return NewWeapon;
    }

    public Room CreateRoom(eRoomType type, Transform parent)
    {
        return roomFactory.CreateRoomToType(type,parent);
    }


    public Monster CreateRandomMonster(Transform parent)
    {
        var newMonster = monsterFactory.CreateRandomMonster(parent);
        return newMonster;
    }

    public Monster CreateMonsterToIndex(int index, Transform parent)
    {
        var newMonster = monsterFactory.CreateMonsterToIndex(index, parent);
        return newMonster;
    }

    public Monster CreateMonsterToLevel(int level, Transform parent)
    {
        var newMonster = monsterFactory.CreateMonsterToLevel(level, parent);
        return newMonster;
    }

    public Fly CreateFly()
    {
        GameObject Fly = Instantiate(FlyPrefab);
        return Fly.GetComponent<Fly>();
    }

}
