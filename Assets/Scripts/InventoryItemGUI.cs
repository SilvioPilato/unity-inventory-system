using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemGUI: MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Sprite icon;
    private int currentSlotIndex = -1;
    public Image image;
    public Sprite Icon
    {
        get => icon;
        set => icon = value;
    }

    public int CurrentSlotIndex
    {
        get => currentSlotIndex;
        set => currentSlotIndex = value;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }

    public void EnableIcon()
    {
        image.enabled = true;
    }

    public void DisableIcon()
    {
        image.enabled = false;
    }

    public void SetIconSize(Vector2 sizeInPixels)
    {
        image.rectTransform.sizeDelta = sizeInPixels;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        DisableIcon();
    }
}
