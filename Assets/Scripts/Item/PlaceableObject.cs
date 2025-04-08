using System;
using UnityEngine;

public class PlaceableObject : AttributeEntity
{
    public GameObject droppedItemPrefab; // ��ӵ� ������ ������
    
    public MeshRenderer meshRenderer;  // ���� ������
    public Texture texture;  //�ؽ�ó

    private void Awake()
    {
        onHit += TakeDamage;
    }

    private void OnDestroy()
    {
        onHit -= TakeDamage;
    }

    void Start()
    {
        attribute.MaxHart = 1;
        attribute.CurHart = attribute.MaxHart;

        // ���� ���� �ؽ�ó ����
        if (meshRenderer != null && texture != null)
        {
            meshRenderer.material.mainTexture = texture;
        }
    }

    public void TakeDamage()
    {
        Debug.Log($"������Ʈ ü��: {attribute.CurHart}/{attribute.MaxHart}");

        if (attribute.CurHart <= 0)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        // ���� ��� �������� ������ �� �ؽ�ó�� �״�� ����
        GameObject drop = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity);
        //drop.GetComponent<DroppedItem>().SetTexture(treeTexture);

        Destroy(gameObject);
    }

}
