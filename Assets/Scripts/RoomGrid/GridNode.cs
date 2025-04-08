using UnityEngine;


public enum eEntityType
{

}

/// <summary>
/// ��忡�� ����,���,�����۵��� ��ġ�ȴ�
/// </summary>
public class GridNode : MonoBehaviour
{
    //�׽�Ʈ�� ���߿� ���� ������
    public GameObject boxItem;
    public GameObject Enemy;

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
        var newEntity = Instantiate(boxItem);
        newEntity.transform.SetParent(this.gameObject.transform);
        newEntity.transform.localPosition = new Vector3(0, 1f, 0);
    }

    public void CreateEnemy()
    {
        var newEntity = Instantiate(Enemy);
        newEntity.transform.localPosition = Vector3.zero;
    }

}
