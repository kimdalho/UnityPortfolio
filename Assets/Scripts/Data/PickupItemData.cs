using UnityEngine;

public enum eTagType
{   
    Stunned, //����
    ninjahead, ///// �ǰ� �� 10% Ȯ���� 1�ʰ� ����
    alienhead, /// 	�ǰ� �� 10% Ȯ���� 1�ʰ� ����
    bearhead, /// �� 10�� óġ �� ü�� 1 ȸ��
    grasshead, // ���� �� 10% Ȯ���� ���� 1 �߰�
    Roostershead, // ����ü�� ���� �� �� ���� 1 �߰�
    clownhair, // ����ü 3�� �߻�
    boxhead, // 3�ʸ��� �ڵ����� ����ü 1�� �߻�
    ninjabody, // �� óġ �� 30�ʰ� ���� 30% ����
    alienbody, // ����ü�� ���� ƨ�� �ݻ��
    beartorso, // ���� �� 10% Ȯ���� ����ü 2�� �¿�� �߰� �߻�
    grasstrunk, // ���ݷ� 20% ���� ����
    Roostersbody, //�� óġ �� 30%Ȯ���� �ִ� ü�� �߰� ȹ��
    clowntorso, // 	��ų ��� �� ���� ����ü�� ���� ȿ�� �߰�
    boxbody, // �̵� �� ù ���ݿ� ����ü 2�� �߰� �߻�

    portalLock,//ȹ��� �̵� �Ұ�
    Attack,
    FanShapeFire // �繫���� ���� ���
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
