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
    public List<PickupWeaponItemData> weaponPickupItemDatas = new List<PickupWeaponItemData>();
    public List<GameObject> WeaponItemPrefab;
    public MonsterFactory monsterFactory;
    public RoomFactory roomFactory;
    public ItemFactory itemFactory;
    public GameObject FlyPrefab;    
    public Dictionary<eWeaponType, RuntimeAnimatorController> dic = new Dictionary<eWeaponType, RuntimeAnimatorController>();




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
    }

    public EquipmentItem CreateItemToTier(int tier, Transform parent)
    {
        var item = itemFactory.CreateItemToTier(tier, parent);
        return item;
    }

    public EquipmentItem CreateItemToIndex(int index, Transform parent)
    {
        var item = itemFactory.CreateItemToIndex(index, parent);
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


    //TODO: 추후 풀링 매니저에서 프레임워크 분리 예정

    //풀링된 오브젝트
    private Dictionary<eRoomType,Queue<Room>> _roomMap;
    //사용중인 오브젝트
    private Dictionary<eRoomType, Queue<Room>> _roomMap2;

    public void CreateRoomPool(Transform parent)
    {
            _roomMap = new Dictionary<eRoomType, Queue<Room>>();
            _roomMap2 = new Dictionary<eRoomType, Queue<Room>>();

            nameless(eRoomType.Start, parent, 1);
            nameless(eRoomType.Item, parent);
            nameless(eRoomType.Monster, parent);
            nameless(eRoomType.NPCRoom, parent, 1);
            nameless(eRoomType.Boss, parent, 1);
            nameless(eRoomType.SacrificeRoom, parent, 1);
               
    }

    private void nameless(eRoomType type, Transform parent,int count = 7)
    {
        Room room = null;        
        _roomMap[type] = new Queue<Room>();
        for (int x = 0; x < count; x++)
        {
            room = roomFactory.CreateRoomToType(type, parent);
            _roomMap[type].Enqueue(room);   
        }             
    }

    public Room GetRoom(eRoomType type)
    {
        var room = _roomMap[type].Dequeue() as Room;
        if( _roomMap2.ContainsKey(type) is false)
        {
            _roomMap2[type] = new Queue<Room>();
        }
        _roomMap2[type].Enqueue(room);
        return room;
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
