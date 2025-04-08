using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    private bool isAttracted = false;
    private Vector3 targetPosition;
    private float attractionSpeed = 0f;
    private float collectDistance = 1.4f;

    public MeshRenderer meshRenderer;
    public Texture itemTexture;
    
    public static ItemData model = new ItemData("나무", "나무 이다", ItemData.eItemType.Resource, 1);

    private void Start()
    {
        //model = new ItemData("나무", "나무 이다", ItemData.eItemType.Resource, 1);
    }

    void Update()
    {
        if (isAttracted)
        {
            attractionSpeed += Time.deltaTime * 5f; // 이동 속도 가속
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, attractionSpeed * Time.deltaTime);

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
        var Inventory = GameObject.Find("Inventory").GetComponent<InvenViewer>();
        if (Inventory == null)
            return;

        Inventory.AddItem(model, 1);
        Debug.Log($"{gameObject.name} 획득!");
        gameObject.SetActive(false);
    }
}
