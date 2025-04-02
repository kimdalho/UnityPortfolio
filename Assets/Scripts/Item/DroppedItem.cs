using System;
using TMPro;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    private bool isAttracted = false;
    private Vector3 targetPosition;
    private float attractionSpeed = 0f;
    private float collectDistance = 1.4f;

    public MeshRenderer meshRenderer;
    public Texture itemTexture;

    void Update()
    {
        if (isAttracted)
        {
            attractionSpeed += Time.deltaTime * 5f; // 이동 속도 가속
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, attractionSpeed * Time.deltaTime);

            Debug.Log((Vector3.Distance(transform.position, targetPosition)));

            if (Vector3.Distance(transform.position, targetPosition) < collectDistance)
            {
                CollectItem();
            }
        }
    }

    public void AttractToPlayer(Vector3 playerPosition, float speedMultiplier)
    {
        isAttracted = true;
        targetPosition = playerPosition;
        attractionSpeed = speedMultiplier;  // 속도 조절
    }

    public void SetTexture(Texture texture)
    {
        itemTexture = texture;
        if (meshRenderer != null && itemTexture != null)
        {
            meshRenderer.material.mainTexture = itemTexture;
        }
    }

    void CollectItem()
    {
        Debug.Log($"{gameObject.name} 획득!");
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
