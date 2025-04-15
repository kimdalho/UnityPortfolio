using UnityEngine;

public enum eTagType
{
    
    Stunned, //����
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
/// ȥ���� ����� ���� �̸����� ���ؼ� �ƽ���
/// �Ӹ��� �ٵ� �����۸��� ����Ѵ�.
/// </summary>
[CreateAssetMenu(fileName = "NewItemData", menuName = "GameData/Item Data")]
public class PickupItemData : ScriptableObject
{
    public eEuipmentType eEquipmentType;

    public ItemData itemData;
    //�÷��̾� �ε���
    public int modelIndex;
    //������Ʈ �ε���
    public int objectIndex;
    //������ ��� ���� ���ϼ��� ���� �������� ����Ǵ°� ����
    public int Rank;

    public eTagType tag;
    public GameAttribute attribute;

}
