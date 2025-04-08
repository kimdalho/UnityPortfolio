using System;
using UnityEngine;

public class PlaceableObject : AttributeEntity
{
    public GameObject droppedItemPrefab; // 드롭될 아이템 프리팹
    
    public MeshRenderer meshRenderer;  // 모델의 렌더러
    public Texture texture;  //텍스처

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

        // 나무 모델의 텍스처 설정
        if (meshRenderer != null && texture != null)
        {
            meshRenderer.material.mainTexture = texture;
        }
    }

    public void TakeDamage()
    {
        Debug.Log($"오브젝트 체력: {attribute.CurHart}/{attribute.MaxHart}");

        if (attribute.CurHart <= 0)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        // 나무 드롭 아이템을 생성할 때 텍스처를 그대로 적용
        GameObject drop = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity);
        //drop.GetComponent<DroppedItem>().SetTexture(treeTexture);

        Destroy(gameObject);
    }

}
