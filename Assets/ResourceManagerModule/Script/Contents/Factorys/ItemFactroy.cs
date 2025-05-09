using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemFactory : MonoBehaviour, IFactory<PickupItemData, EquipmentItem>
{
    public List<PickupItemData> pickupItemDatas = new List<PickupItemData>();

    public EquipmentItem CreateItemToIndex(int index, Transform parent)
    {
        if (index > pickupItemDatas.Count - 1)
            return null;

        var data = pickupItemDatas[index];
        return Create(data, parent);
    }

    public EquipmentItem CreateItemToTier(int tier, Transform parent)
    {
        System.Random rand = new System.Random();
        var data = pickupItemDatas.Where(_ => _.Rank == tier)
        .OrderBy(x => rand.Next())
        .Take(1)
        .SingleOrDefault();

        return Create(data, parent);
    }

    public EquipmentItem Create(PickupItemData data, Transform parent)
    {
        GameObject go = Instantiate(data.prefab,parent);
        EquipmentItem item = go.GetComponent<EquipmentItem>();
        item.transform.SetParent(parent);
        item.transform.localScale = Vector3.one;          
        item.Init(data);
        return item;
    }
}