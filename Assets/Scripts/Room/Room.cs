using UnityEngine;
using System.Collections.Generic;
public enum eDirection
{
    Top,
    Bottom,
    Left,
    Right
}

public class Room : MonoBehaviour
{
    public string Guid;
    public eRoomType roomType;

    public Dictionary<eDirection, Portal> keyValuePairs = new Dictionary<eDirection, Portal>();
    public Portal top, bottom, left, right;
    public Portal Next;

    
    public RoomGrid grid;

       
    public void Init(RoomData model)
    {
        Guid = model.guid;        
        string str_name = string.Format("{0},{1}", model.roomType.ToString(), this.Guid);
        gameObject.name = str_name;
        this.roomType = model.roomType;

        top.gameObject.name = eDirection.Top.ToString();
        bottom.gameObject.name = eDirection.Bottom.ToString();
        left.gameObject.name = eDirection.Left.ToString();
        right.gameObject.name = eDirection.Right.ToString();

        transform.position = new Vector3(model.position.x, 0, model.position.y);

        keyValuePairs.Add(eDirection.Top, top);
        keyValuePairs.Add(eDirection.Right, right);
        keyValuePairs.Add(eDirection.Left, left);
        keyValuePairs.Add(eDirection.Bottom, bottom);

        SetDatatoType();
    }

    public void SetDatatoType()
    {
     
        switch (roomType)
        {
            case eRoomType.Boss:
                Next.gameObject.SetActive(true);
                break;
            case eRoomType.Monster:
                break;
            case eRoomType.Start:
                transform.position = Vector3.zero;
                break;
        }
    }



}
