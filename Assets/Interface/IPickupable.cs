using UnityEngine;
/// <summary>
/// �÷��̾ ���������� ������ ȹ���� ������ Ÿ���� ��ƼƼ���� ���
/// </summary>
public interface IPickupable
{
    void OnPickup(Character character);

    (ItemData, AttributeSet) GetItemData();
    
}