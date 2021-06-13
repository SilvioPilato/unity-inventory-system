using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemGUI: MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Sprite icon;
    private int currentSlotIndex = -1;
    public Image image;
    private Vector2 iconSize;
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

    public Vector2 IconSize
    {
        get => iconSize;
        set => iconSize = value;
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
        UpdateIcon();
        image.enabled = true;
    }

    public void DisableIcon()
    {
        image.enabled = false;
    }

    private void UpdateIcon()
    {
        image.sprite = Icon;
        SetIconSize(iconSize);
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

    private void Start()
    {
        UpdateIcon();
    }
}
