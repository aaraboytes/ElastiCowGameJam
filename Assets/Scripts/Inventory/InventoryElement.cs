using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    public bool Selected { get { return _selector.enabled; } set { _selector.enabled = value; } }
    public InventoryUI Inventory { set { inventory = value; } }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }

    [SerializeField] Image _sprite;
    [SerializeField] Image _selector;
    [SerializeField] Button _button;

    private InventoryUI inventory;
    private ItemData itemData;

    private void Start()
    {
        _button.onClick.AddListener(Select);
    }

    private void Select()
    {
        inventory.Select(this);
    }

    public void Populate(ItemData item)
    {
        itemData = item;
        _sprite.sprite = item.ItemImage;
    }
}
