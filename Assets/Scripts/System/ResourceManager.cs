
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 무기 열거타입에 오해가 있으면안된다.
/// 모델의 웨폰 인덱스는 0요소값에 라이플로 시작이다.
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

    public List<PickupItemData> pickupItemDatas = new List<PickupItemData>();

    public GameObject HeadItemPrefab;
    public GameObject BodyItemPrefab;

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

    public GameObject CreateHeadItem(int index ,Transform parent)
    {
        PickupItemData data = pickupItemDatas[index];
        GameObject item = Instantiate(HeadItemPrefab);        
        item.transform.SetParent(parent, false);
        item.transform.localPosition = new Vector3(0, 2, 0);
        item.transform.SetParent(null);
        item.transform.localScale = Vector3.one;
        var itemCompo = item.GetComponent<HeadItem>();        
        itemCompo.Init(pickupItemDatas[index]);
        return item;
    }

    public GameObject CreateBodyItem(int index, Transform parent)
    {
        PickupItemData data = pickupItemDatas[index];
        GameObject item = Instantiate(BodyItemPrefab);
        item.transform.SetParent(parent, false);
        item.transform.localPosition = new Vector3(0, 2, 0);
        item.transform.SetParent(null);
        item.transform.localScale = Vector3.one;
        var itemCompo = item.GetComponent<BodyItem>();
        itemCompo.Init(pickupItemDatas[index]);
        return item;
    }

}
