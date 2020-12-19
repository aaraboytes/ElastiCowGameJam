using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject _element;
    [SerializeField] PlayerItems _items;
    [SerializeField] Transform _container;
    [SerializeField] Image _selectedImage;
    [SerializeField] TextMeshProUGUI _selectedTitle;
    [SerializeField] TextMeshProUGUI _selectedDescription;

    private InventoryElement selected;
    private Dictionary<ItemData, InventoryElement> inventoryItems = new Dictionary<ItemData, InventoryElement>();

    private void OnEnable()
    {
        List<ItemData> playerItems = _items.Items;
        List<ItemData> totalItems = new List<ItemData>();
        totalItems.AddRange(playerItems);
        totalItems.AddRange(inventoryItems.Keys);
        foreach(var item in totalItems)
        {
            //Spawn inventory items
            if (!inventoryItems.ContainsKey(item))
            {
                Debug.Log("Spawning ui element " + item.Name);
                InventoryElement newElement = Instantiate(_element, _container).GetComponent<InventoryElement>();
                newElement.Inventory = this;
                newElement.Populate(item);
                newElement.gameObject.SetActive(true);
                inventoryItems[item] = newElement;
            }
            inventoryItems[item].gameObject.SetActive(playerItems.Contains(item));
        }
        _selectedTitle.text = "";
        _selectedDescription.text = "";
        _selectedImage.sprite = null;
        if (selected) selected.Selected = false;
        selected = null;
        
        for (int i = 0; i < _container.childCount; i++)
        {
            if (_container.GetChild(i).gameObject.activeInHierarchy)
            {
                InventoryElement element = _container.GetChild(i).GetComponent<InventoryElement>();
                Select(element);
                break;
            }
        }
    }
    public void Select(InventoryElement element)
    {
        if(selected) selected.Selected = false;
        selected = element;
        selected.Selected = true;
        _selectedImage.sprite = selected.ItemData.ItemImage;
        _selectedTitle.text = selected.ItemData.Name;
        _selectedDescription.text = selected.ItemData.Description;
    }
}
