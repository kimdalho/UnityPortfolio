using UnityEngine;


/// <summary>
/// ��忡�� ����,���,�����۵��� ��ġ�ȴ�
/// </summary>
public class GridNode : MonoBehaviour
{
    public bool exist = false;
    /// <summary>
    /// Ÿ���� � ���̺����͸� �������� �˷��ش�
    /// id�� �ش� ������ ���̺� �������� �������̴�.
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
