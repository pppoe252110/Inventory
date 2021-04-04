using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public int VerticalCellsCount = 9;
    public int HorizontalCellsCount = 9;
    public List<InventoryCell> inventoryCells = new List<InventoryCell>();
    public GameObject ItemPrefab;
    public Transform Content;
    public Image MouseItemIcon;
    public static InventoryManager Instance;

    public InventoryCell EnteredCell;
    public InventoryCell DraggingCell;
    public bool isDraggingItem;

    [Header("Debug")]
    public Item TestItemApple;
    public Item TestItemBanana;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        FillInventory();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Tuple<bool, int> tuple = GetFreeCell();
            if (tuple.Item1)
            {
                inventoryCells[tuple.Item2].CurrentItem = TestItemApple;
                UpdateCells();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Tuple<bool, int> tuple = GetFreeCell();
            if (tuple.Item1)
            {
                inventoryCells[tuple.Item2].CurrentItem = TestItemBanana;
                UpdateCells();
            }
        }
        if (isDraggingItem&&DraggingCell.CurrentItem)
        {
            MouseItemIcon.sprite = DraggingCell.CurrentItem.Icon;
            MouseItemIcon.color = Color.white;
            MouseItemIcon.rectTransform.position = Input.mousePosition;
        }
        else
        {
            MouseItemIcon.color = Color.clear;
        }
    }
    public void FillInventory()
    {
        for (int i = 0; i < VerticalCellsCount*HorizontalCellsCount; i++)
        {
            InventoryCell cell = Instantiate(ItemPrefab, Content).GetComponent<InventoryCell>();
            inventoryCells.Add(cell);
        }
    }
    public Tuple<bool,int> GetFreeCell()
    {
        for (int i = 0; i < inventoryCells.Count; i++)
        {
            if (inventoryCells[i].CurrentItem == null)
            {
                return Tuple.Create(true,i);
            }
        }
        Debug.LogError("There is not any free cell");
        return Tuple.Create(false, 0);
    }
    public void AddItem(Item item,int cellId)
    {
        inventoryCells[cellId].CurrentItem = item;
    }
    public void UpdateCells()
    {
        for (int i = 0; i < inventoryCells.Count; i++)
        {
            inventoryCells[i].UpdateCell();
        }
    }
}
