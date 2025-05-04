using UnityEngine;

public enum eTagType
{   
    Stunned, //����
    ninjahead, ///// 
    alienhead, ///
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

    Attack,
    Attacking, //������
    FanShapeFire, // ����ü �߻� (Ninja, Soldier ���)
    FlameThrower,  // ���� �ð����� ȭ�� ��� (Burner ���)
    NinjaHead_State_Invincible, //

    NinjaBody_State_SpeedUp,// ���ڹٵ� ������Ʈ

    Equip_Weapon_State_default, //�÷��̾ ������ ������ Ÿ�� üũ
    Equip_Weapon_State_Rifl, //�÷��̾ ������ ������ Ÿ�� üũ
    Equip_Weapon_State_Bazooka, //�÷��̾ ������ ������ Ÿ�� üũ
    Equip_Weapon_State_Handgun, //�÷��̾ ������ ������ Ÿ�� üũ

    Player_State_IgnoreInput, // �긴�� Ÿ���ִ��� �̶��� �Է¹�������
    Player_State_IgnorePortal, // �긴�� Ÿ���ִ��� �̶��� �Է¹�������
    Player_State_HasAttackTarget, // ���� ����� �����Ѵ�.

    Effect_NinjaSkill, //���� ��ų �ߵ� ��

}


/// <summary>
/// ȥ���� ����� ���� �̸����� ���ؼ� �ƽ���
/// �Ӹ��� �ٵ� �����۸��� ����Ѵ�.
/// </summary>
[CreateAssetMenu(fileName = "NewItemData", menuName = "GameData/Item Data")]
public class PickupItemData : PickupItemDataBase
{
    //�÷��̾� �ε���
    public int modelIndex;

    public GameAttribute attribute;

    //������Ʈ �ε���
    public int objectIndex;
    //������ ��� ���� ���ϼ��� ���� �������� ����Ǵ°� ����    
    public int Rank;
}

