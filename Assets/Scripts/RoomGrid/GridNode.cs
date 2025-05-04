using UnityEngine;


public enum eEntityType
{

}

/// <summary>
/// ��忡�� ����,���,�����۵��� ��ġ�ȴ�
/// </summary>
public class GridNode : MonoBehaviour
{
    public GameObject wall;
    public bool exist = false;
    /// <summary>
    /// Ÿ���� � ���̺����͸� �������� �˷��ش�
    /// id�� �ش� ������ ���̺� �������� �������̴�.
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
