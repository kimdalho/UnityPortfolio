using UnityEngine;
/// <summary>
/// 플레이어가 물리적으로 닿으면 획득이 가능한 타입의 엔티티에게 상속
/// </summary>
public interface IPickupable
{
    void OnPickup(Character character, GameObject picker);

    (ItemData, GameAttribute) GetItemData();
    
}