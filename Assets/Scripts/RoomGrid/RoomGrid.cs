using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RoomGrid : MonoBehaviour
{
    [SerializeField]
    private List<GridRow> grid;

    public List<GridRow> GetGrid()
    {
        return grid;
    }

}
