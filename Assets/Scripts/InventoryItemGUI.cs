using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class InventoryItemGUI: MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image image;
    public Sprite Icon { get; set; }
    public int CurrentSlotIndex { get; set; } = -1;
    public event Action<InventoryItemGUI> OnClick;
    public PointerEventData.InputButton onClickButton = PointerEventData.InputButton.Right;
    public Vector2 IconSize { get; set; }
    private CanvasGroup _canvasGroup;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
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
        _canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        DisableIcon();
    }

    private void Start()
    {
        UpdateIcon();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != onClickButton) return;
        OnClick?.Invoke(this);
    }
}
