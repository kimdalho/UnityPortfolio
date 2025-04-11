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


    private void Start()
    {
        CreateItem();
    }

    public void CreateItem() 
    {
        int index = 0;
        foreach (GridRow row in grid)
        {
            foreach(GridNode node in row.GetGrid())
            {                
                if(index < 6)
                {
                    ResourceManager.Instance.CreateHeadItem(index, node.transform);
                }
                index++;

                if(index >= 6 && index < 12)
                {
                    ResourceManager.Instance.CreateBodyItem(index - 6, node.transform);
                }


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
