using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public List<ItemData> Items => items;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] private List<ItemData> items = new List<ItemData>();
    [SerializeField] FPSPlayer _movement;
    private IO_MilkBucket bucket;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool inventoryEnabled = inventoryUI.gameObject.activeInHierarchy;
            inventoryEnabled = !inventoryEnabled;
            inventoryUI.gameObject.SetActive(inventoryEnabled);
            _movement.enabled = !inventoryEnabled;
            Cursor.visible = inventoryEnabled;
        }
    }
    public void AddItem(ItemData newItem)
    {
        items.Add(newItem);
    }
    public void FillBucket()
    {
        bucket.Fill();
    }
    public void Remove(ItemData item)
    {
        Debug.Log("Removed " + item.Name);
        items.Remove(item);
    }
    public bool Contains(ItemData item) => items.Contains(item);
}
