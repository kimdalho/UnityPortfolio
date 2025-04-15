using UnityEngine;

public enum eTagType
{
    
    Stunned, //스턴
    ninjahead,
    alienhead,
    bearhead,
    grasshead,
    Roostershead,
    clownhair,
    boxhead,
    ninjabody,
    alienbody,
    beartorso,
    grasstrunk,
    Roostersbody,
    clowntorso,
    boxbody,
    portalLock,
}


/// <summary>
/// 혼선이 생기기 쉬운 이름으로 정해서 아쉽다
/// 머리와 바디 아이템만을 취급한다.
/// </summary>
[CreateAssetMenu(fileName = "NewItemData", menuName = "GameData/Item Data")]
public class PickupItemData : ScriptableObject
{
    public eEuipmentType eEquipmentType;

    public ItemData itemData;
    //플레이어 인덱스
    public int modelIndex;
    //오브젝트 인덱스
    public int objectIndex;
    //아이템 등급 높은 템일수록 상위 층에서만 드랍되는게 조건
    public int Rank;

    public eTagType tag;
    public GameAttribute attribute;

}
