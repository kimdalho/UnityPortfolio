using System;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private readonly String STR_PLAYER = "Player";

    public GameObject droppedItemPrefab; // ��ӵ� ������ ������
    public int maxHealth = 3; // ������
    private int currentHealth;
    public MeshRenderer meshRenderer;  // ���� ������
    public Texture texture;  //�ؽ�ó

    void Start()
    {
        currentHealth = maxHealth;

        // ���� ���� �ؽ�ó ����
        if (meshRenderer != null && texture != null)
        {
            meshRenderer.material.mainTexture = texture;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"������Ʈ ü��: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(STR_PLAYER))
        {
            DestroyObject();
        }
    }

}
