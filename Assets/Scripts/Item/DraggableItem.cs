using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public InventorySlot originSlot;
    private Transform parentBeforeDrag;
    public CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventorySlot targetSlot = FindSlotUnderPointer(eventData);
        if (targetSlot != null)
        {
            Debug.Log(targetSlot);
            originSlot.SwapItems(targetSlot);
        }
        

        transform.SetParent(parentBeforeDrag);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;

    }
    private InventorySlot FindSlotUnderPointer(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results); // 마우스 위치에 있는 UI 요소 검색

        foreach (var result in results)
        {
            InventorySlot slot = result.gameObject.GetComponent<InventorySlot>();
            if (slot != null)
            {
                return slot; // 드롭된 슬롯 반환
            }
        }

        return null; // 적절한 슬롯이 없음
    }
}