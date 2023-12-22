using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public int INVENTORY_CAPACITY = 9;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void addItem(Item item)
    {
        Items.Add(item);
    }

    public void removeItem(Item item)
    {
        Items.Remove(item);
    }

    public void listItems()
    {
        cleanList();

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemValue = obj.GetComponentInChildren<Text>();
            var itemIcon = obj.GetComponentInChildren<Image>();

            itemValue.text = item.id.ToString();
            itemIcon.sprite = item.icon;
        }

    }

    public void removeItemByButton(int id)
    {
        Item itemToRemove = null;

        foreach (var item in Items)
        {
            if(item.id == id)
            {
                itemToRemove = item;
                break;
            }
        }

        if (itemToRemove != null)
        {
            Items.Remove(itemToRemove);
        }

    }

    public void cleanList()
    {
        // clean content before opening
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }
}
