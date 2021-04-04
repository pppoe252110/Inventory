using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Item CurrentItem;
    public Image Image;
    public bool IsDragging;
    public bool IsEntered;
    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponentsInChildren<Image>()[1];
        UpdateCell();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)&&IsEntered)
        {
            RemoveItem();
        }
    }
    public void UpdateCell()
    {
        if (CurrentItem && CurrentItem.Icon)
        {
            Image.sprite = CurrentItem.Icon;
            Image.color = Color.white;
        }
        else
        {
            Image.sprite = null;
            Image.color = Color.clear;
        }
    }

    public void RemoveItem()
    {
        CurrentItem = null;
        UpdateCell();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //this method using for OnBeginDrag and OnEndDrag events
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
        InventoryManager.Instance.isDraggingItem = true;
        InventoryManager.Instance.DraggingCell = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsDragging&&InventoryManager.Instance.EnteredCell)
        {
            Item item = InventoryManager.Instance.EnteredCell.CurrentItem;
            InventoryManager.Instance.EnteredCell.CurrentItem = CurrentItem;
            CurrentItem = item;
            InventoryManager.Instance.isDraggingItem = false;
            InventoryManager.Instance.DraggingCell = null;
            InventoryManager.Instance.EnteredCell = null;
            IsDragging = false;
        }
        else if (IsDragging)
        {
            InventoryManager.Instance.isDraggingItem = false;
            InventoryManager.Instance.DraggingCell = null;
            InventoryManager.Instance.EnteredCell = null;
            IsDragging = false;
        }
        InventoryManager.Instance.UpdateCells();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsEntered = true;
        InventoryManager.Instance.EnteredCell = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsEntered = false;
        InventoryManager.Instance.EnteredCell = null;
    }
}
