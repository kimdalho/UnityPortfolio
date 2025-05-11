using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ���� ����Ÿ�Կ� ���ذ� ������ȵȴ�.
/// ���� ���� �ε����� 0��Ұ��� �����÷� �����̴�.
/// </summary>


public class ResourceManager : MonoBehaviour
{    
    private static ResourceManager _instance;    
    /// <summary>
    /// ������ �б� Ÿ�Կ� ���� ���񽺰� �޶�����.
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


    //TODO: ���� Ǯ�� �Ŵ������� �����ӿ�ũ �и� ����

    //Ǯ���� ������Ʈ
    private Dictionary<eRoomType,Queue<Room>> _roomMap;
    //������� ������Ʈ
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
