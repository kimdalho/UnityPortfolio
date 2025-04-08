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

}
