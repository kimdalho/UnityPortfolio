using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GridRow : MonoBehaviour
{
    //public List<>
    [SerializeField]
    private List<GridNode> listGrid;

    public List<GridNode> GetGrid()
    { 
        return listGrid;
    }
    public void RepositionChildren()
    {
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            child.localPosition = new Vector3(i, 0, 0); // (0,0,0), (1,0,0), (2,0,0) ...
        }

        Debug.Log("자식 객체 위치 재배치 완료");
    }

    public void SetList()
    {
        listGrid.Clear();
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            GridNode node =  child.GetComponent<GridNode>();
            if (node != null)
            {
                listGrid.Add(node);
            }
        }

        Debug.Log("자식 객체 위치 재배치 완료");
    }

    public void CreateWall()
    {
        foreach (GridNode node in listGrid)
        {
            node.CreateWall();
        }
    }
}
