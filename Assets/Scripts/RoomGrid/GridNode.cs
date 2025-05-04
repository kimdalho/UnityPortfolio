using UnityEngine;


public enum eEntityType
{

}

/// <summary>
/// 노드에는 몬스터,기믹,아이템들이 배치된다
/// </summary>
public class GridNode : MonoBehaviour
{
    public GameObject wall;
    public bool exist = false;
    /// <summary>
    /// 타입은 어떤 테이블데이터를 참조할지 알려준다
    /// id는 해당 참조할 테이블 데이터의 데이터이다.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="id"></param>
    public void SetData(eEntityType type,int id)
    {

    }

    public void CreateBox()
    {
        var newEntity = Instantiate(wall);
        newEntity.transform.SetParent(this.gameObject.transform);
        newEntity.transform.localPosition = new Vector3(0, 1f, 0);
    }

    
    public void CreateWall()
    {
        var newEntity = Instantiate(wall);
        newEntity.transform.SetParent(this.gameObject.transform);
        newEntity.transform.localPosition = new Vector3(0, 1f, 0);
    }

    public Vector3 GetItemPos()
    {
        return new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
      
    }


}
