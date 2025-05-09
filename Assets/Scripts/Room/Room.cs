using UnityEngine;
using System.Collections.Generic;
public enum eDirection
{
    Top,
    Bottom,
    Left,
    Right
}

public class Room : MonoBehaviour , IInitializableItem<RoomPrefabSO>
{
    public string Guid;
    public eRoomType roomType;

    public Dictionary<eDirection, Portal> keyValuePairs = new Dictionary<eDirection, Portal>();
    public Portal top, bottom, left, right;
    public Portal Next;    
    public RoomGrid grid;
    
    public List<PlayerTraversal> traversals;
    public List<Monster> roomMonsters;

    public bool isClear = false;

    //런타임중에 사용되는 포탈들
    public List<Portal> enablePortal;
       
    public void SetData(RoomPrefabSO model)
    {     
        string str_name = string.Format("{0},{1}", model.roomType.ToString(), this.Guid);
        gameObject.name = str_name;
        this.roomType = model.roomType;

        top.gameObject.name = eDirection.Top.ToString();
        bottom.gameObject.name = eDirection.Bottom.ToString();
        left.gameObject.name = eDirection.Left.ToString();
        right.gameObject.name = eDirection.Right.ToString();
        
        keyValuePairs.Add(eDirection.Top, top);
        keyValuePairs.Add(eDirection.Right, right);
        keyValuePairs.Add(eDirection.Left, left);
        keyValuePairs.Add(eDirection.Bottom, bottom);

        SetDatatoType();        
    }

    //몬스터가 죽을때마다 해당 룸에서 클리어인지 체크한다.
    public void OnMonsterDeath(Monster deadMonster)
    {
        if (IsAllDead())
        {
            OpenToRoomPortals();
        }
    }

    private bool IsAllDead()
    {
        foreach (var monster in roomMonsters)
        {
            if (!monster.isDead)
                return false;
        }
        isClear = true;
        return true;
    }

    public void SetDatatoType()
    {     
        switch (roomType)
        {
            case eRoomType.Boss:
                Next.gameObject.SetActive(false);
                break;
            case eRoomType.Start:
                transform.position = Vector3.zero;
                break;
        }
    }

    public void DisableRoom()
    {
        foreach (Portal portal in enablePortal)
        {
            portal.gameObject.SetActive(false);
        }

        foreach (PlayerTraversal traversal in traversals)
        {
            traversal.gameObject.SetActive(false);
        }
    }
    public void OpenToRoomPortals()
    {
        if (!isClear)
            return;

        if (this != GameManager.instance.curRoom)
            return;

        foreach (Portal portal in enablePortal)
        {
            portal.gameObject.SetActive(true);
        }        


        foreach (PlayerTraversal traversal in traversals)
        {
            traversal.gameObject.SetActive(true);
        }
    }

    

}
