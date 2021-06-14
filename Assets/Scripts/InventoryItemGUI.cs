using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class InventoryItemGUI: MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Image image;

    public Sprite Icon { get; set; }

    public int CurrentSlotIndex { get; set; } = -1;

    public Vector2 IconSize { get; set; }
    private CanvasGroup canvasGroup;
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        canvasGroup.blocksRaycasts = false;
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
        SetIconSize(IconSize);
    }

    private void SetIconSize(Vector2 sizeInPixels)
    {
        image.rectTransform.sizeDelta = sizeInPixels;
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        DisableIcon();
    }

    private void Start()
    {
        UpdateIcon();
    }
}
