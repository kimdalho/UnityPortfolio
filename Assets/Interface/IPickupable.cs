using UnityEngine;
/// <summary>
/// �÷��̾ ���������� ������ ȹ���� ������ Ÿ���� ��ƼƼ���� ���
/// </summary>
public interface IPickupable
{
    void OnPickup(Character character, GameObject picker);

    (ItemData, GameAttribute) GetItemData();
    
}