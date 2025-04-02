using System;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private readonly String STR_PLAYER = "Player";

    public GameObject droppedItemPrefab; // 드롭될 아이템 프리팹
    public int maxHealth = 3; // 내구도
    private int currentHealth;
    public MeshRenderer meshRenderer;  // 모델의 렌더러
    public Texture texture;  //텍스처

    void Start()
    {
        currentHealth = maxHealth;

        // 나무 모델의 텍스처 설정
        if (meshRenderer != null && texture != null)
        {
            meshRenderer.material.mainTexture = texture;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"오브젝트 체력: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(STR_PLAYER))
        {
            DestroyObject();
        }
    }

}
