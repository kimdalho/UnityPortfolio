using UnityEngine;


/// <summary>
/// 노드에는 몬스터,기믹,아이템들이 배치된다
/// </summary>
public class GridNode : MonoBehaviour
{
    public bool exist = false;
    /// <summary>
    /// 타입은 어떤 테이블데이터를 참조할지 알려준다
    /// id는 해당 참조할 테이블 데이터의 데이터이다.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="id"></param>
    public void SetData(int x, int y)
    {

    }


    public Vector3 GetItemPos()
    {
        
        return new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
      
    }


}
