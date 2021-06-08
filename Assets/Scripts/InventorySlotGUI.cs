using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotGUI: MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"You clicked on {gameObject.name}");
    }
}
