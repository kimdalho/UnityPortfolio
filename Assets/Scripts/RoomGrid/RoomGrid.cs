using System.Collections.Generic;
using UnityEngine;


public class RoomGrid : MonoBehaviour
{
    [SerializeField]
    private List<GridRow> grid;

    public void RepositionChildren()
    {
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            child.localPosition = new Vector3(0, 0, i); // (0,0,0), (1,0,0), (2,0,0) ...
        }

        Debug.Log("자식 객체 위치 재배치 완료");
    }

    public GridNode GetRandomGridNode()
    {
        System.Random rand1 = new System.Random();
        System.Random rand2 = new System.Random();
        GridRow selectRowGrid = grid[rand1.Next(grid.Count)];
        List<GridNode> result  = selectRowGrid.GetGrid();
        return result[rand2.Next(selectRowGrid.GetGrid().Count)];
    }
    
    public Monster CreateMonster(Transform parent)
    {        
        GridNode node = GetRandomGridNode();
        GameObject newmon =  ResourceManager.Instance.CreateMonster(parent);
        node.gameObject.name = "XX1";
        Monster newMonsterCompo = newmon.GetComponent<Monster>();
        newMonsterCompo.startNode = node;
        newMonsterCompo.SetRoomGrid(transform);       
        return newMonsterCompo;

    }



    

    /// <summary>
    /// 아이템 테스트를 위해서 생성 스타트에서 GameScene에서 Start 함수로 사용
    /// </summary>
    public GameObject CreateItem(Transform parent, int tier) 
    {
        GridNode node = GetRandomGridNode();
        node.exist = true;
        GameObject newItem = ResourceManager.Instance.CreateItemToTier(tier, parent);
        newItem.transform.position = node.GetItemPos();
        return newItem;
    }

    private void Start()
    {
        //ItemTest();
        //WeaponTest();
    }
    
    public void ItemTest()
    {
        int index = 0;
        foreach (var rownode in grid)
        {
            foreach (var node in rownode.GetGrid())
            {
                var newItem = ResourceManager.Instance.CreateItemToIndex(index, node.transform);
                if(newItem == null)
                {
                    return;
                }
                newItem.transform.position = node.GetItemPos();
                index++;
            }
        }
    }

    public void WeaponTest()
    {
        int index = 0;
        foreach (var rownode in grid)
        {
            foreach (var node in rownode.GetGrid())
            {
                var newItem = ResourceManager.Instance.CreateweaponItemToIndex(index, node.transform);
                if (newItem == null)
                {
                    return;
                }
                newItem.transform.position = node.GetItemPos();
                index++;
            }
        }
    }


    public void SetList()
    {
        grid.Clear();
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            GridRow node = child.GetComponent<GridRow>();
            if (node != null)
            {
                grid.Add(node);
            }
        }

        Debug.Log("자식 객체 위치 재배치 완료");
    }

}
